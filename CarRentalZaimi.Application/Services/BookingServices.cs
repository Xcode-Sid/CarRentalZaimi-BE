using AutoMapper;
using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Common.Messages;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.BookingRequest.Commands.AcceptBooking;
using CarRentalZaimi.Application.Features.BookingRequest.Commands.CancelBooking;
using CarRentalZaimi.Application.Features.BookingRequest.Commands.CloseBooking;
using CarRentalZaimi.Application.Features.BookingRequest.Commands.CreateBookingRequest;
using CarRentalZaimi.Application.Features.BookingRequest.Commands.RefuseBooking;
using CarRentalZaimi.Application.Features.BookingRequest.Queries.GetAllBookings;
using CarRentalZaimi.Application.Features.BookingRequest.Queries.GetAllUserBookings;
using CarRentalZaimi.Application.Interfaces.Services;
using CarRentalZaimi.Application.Interfaces.UnitOfWork;
using CarRentalZaimi.Domain.Common.Constants;
using CarRentalZaimi.Domain.Entities;
using CarRentalZaimi.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CarRentalZaimi.Application.Services;

public class BookingServices(IUnitOfWork _unitOfWork, IMapper _mapper, IEmailService _emailService, 
    ILogger<BookingServices> _logger, UserManager<User> _userManager, INotificationService _notificationService) : IBookingServices
{
    public async Task<Result<BookingDto>> CreateBookingRequestAsync(CreateBookingRequestCommand request, CancellationToken cancellationToken = default)
    {
        var user = await _unitOfWork.Repository<User>()
            .FirstOrDefaultAsync(p => p.Id == request.UserId, cancellationToken);

        if (user is null)
            return Result<BookingDto>.Error(ServiceErrorMessages.User.NotFound);

        var car = await _unitOfWork.Repository<Car>()
            .AsQueryable()
            .Include(c => c.Name)
            .Include(c => c.Model)
            .FirstOrDefaultAsync(p => p.Id.ToString() == request.CarId, cancellationToken);

        if (car is null)
            return Result<BookingDto>.Error(ServiceErrorMessages.Car.NotFound);

        Enum.TryParse<PaymentMethod>(request.PaymentMethod, ignoreCase: true, out var paymentMethod);

        await _unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            var booking = new Booking
            {
                Reference = GenerateBookingReference(),
                PhoneNumber = request.PhoneNumber,
                PaymentMethod = paymentMethod,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Status = BookingStatus.Accepted,
                TotalPrice = request.TotalPrice,
                User = user,
                Car = car,
            };

            foreach (var serviceId in request.AditionalServiceIds!)
            {
                var service = await _unitOfWork.Repository<AdditionalService>()
                    .FirstOrDefaultAsync(p => p.Id.ToString() == serviceId, cancellationToken);

                var bookingServices = new BookingService
                {
                    Booking = booking,
                    AdditionalService = service
                };
                await _unitOfWork.Repository<BookingService>().AddAsync(bookingServices, cancellationToken);
            }

            await _unitOfWork.Repository<Booking>().AddAsync(booking, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await _unitOfWork.CommitTransactionAsync(cancellationToken);

            // Notify admin via SignalR and Persistence
            await _notificationService.SendNotificationToAdminsAsync(
                $"{user.FirstName} {user.LastName} has made a booking request for {car.Name?.Name} {car.Model?.Name}.",
                UserNotificationType.NewBookingRequest);

            // Send email outside transaction — no need to roll back if email fails
            var adminUsers = await _userManager.GetUsersInRoleAsync(SystemPolicies.Admin);
            var admin = adminUsers.FirstOrDefault();
            if (admin is not null)
            {
                var emailResult = await _emailService.SendBookingRequestEmailToAdminAsync(
                    admin.Email!,
                    $"{user.FirstName} {user.LastName}",
                    $"{booking.Car.Name?.Name} {booking.Car.Model?.Name}",
                    booking.Reference,
                    cancellationToken);

                if (!emailResult.IsSuccessful)
                    _logger.LogWarning("Failed to send admin booking notification for reference {Ref}", booking.Reference);
            }

            return Result<BookingDto>.Success(_mapper.Map<BookingDto>(booking));
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            _logger.LogError(ex, "Failed to create booking request for user {UserId}", request.UserId);
            return Result<BookingDto>.Error(ServiceErrorMessages.Booking.CreateFailed);
        }
    }

    public async Task<Result<bool>> CancelBookingAsync(CancelBookingCommand request, CancellationToken cancellationToken = default)
    {

        var booking = await _unitOfWork.Repository<Booking>()
            .AsQueryable()
            .Include(b => b.User)
            .Include(b => b.Car)
            .Include(b => b.Car!.Model)
            .Include(b => b.Car!.Name)
            .FirstOrDefaultAsync(p => p.Id.ToString() == request.BookingId, cancellationToken);

        if (booking is null)
            return Result<bool>.Error(ServiceErrorMessages.Booking.NotFound);

        if (booking.Status == BookingStatus.Refused)
            return Result<bool>.Error(ServiceErrorMessages.Booking.AlreadyRefusedCannotCancel);

        await _unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            booking.IsCanceled = true;
            booking.Status = BookingStatus.Refused;
            booking.RefuzedBy = RefuzedByType.User;
            booking.RefuzedReason = request.Reason;

            await _unitOfWork.Repository<Booking>().UpdateAsync(booking, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await _unitOfWork.CommitTransactionAsync(cancellationToken);

            // Notify admin
            await _notificationService.SendNotificationToAdminsAsync(
                $"{booking.User!.FirstName} {booking.User!.LastName} has cancelled their booking for {booking.Car!.Name?.Name} {booking.Car.Model?.Name}.",
                UserNotificationType.BookingCancelled);

            // Send email outside transaction — no need to roll back if email fails
            var adminUsers = await _userManager.GetUsersInRoleAsync(SystemPolicies.Admin);
            var admin = adminUsers.FirstOrDefault();
            if (admin is not null)
            {
                var emailResult = await _emailService.SendBookingCancellationEmailToAdminAsync(
                    admin.Email!,
                    $"{booking.User!.FirstName} {booking.User!.LastName}",
                    $"{booking.Car!.Title}",
                    $"{booking.Car.Name?.Name} {booking.Car.Model?.Name}",
                    DateTime.UtcNow.ToString("dd MMM yyyy"),
                    booking.RefuzedReason!,
                    cancellationToken);

                if (!emailResult.IsSuccessful)
                    _logger.LogWarning("Failed to send cancellation email to admin for reference {Ref}", booking.Reference);
            }

            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            _logger.LogError(ex, "Failed to cancel booking {BookingId}", request.BookingId);
            return Result<bool>.Error(ServiceErrorMessages.Booking.CancelFailed);
        }
    }

    public async Task<Result<BookingDto>> AcceptBookingRequestAsync(AcceptBookingCommand request, CancellationToken cancellationToken = default)
    {
       
        var booking = await _unitOfWork.Repository<Booking>()
            .AsQueryable()
            .Include(b => b.Car)
            .Include(b => b.Car!.Model)
            .Include(b => b.Car!.Name)
            .Include(b => b.User)
            .FirstOrDefaultAsync(p => p.Id.ToString() == request.BookingId, cancellationToken);

        if (booking is null)
            return Result<BookingDto>.Error(ServiceErrorMessages.Booking.NotFound);

        if (booking.Status == BookingStatus.Done)
            return Result<BookingDto>.Error(ServiceErrorMessages.Booking.AlreadyDoneCannotAccept);

        if (booking.IsCanceled)
            return Result<BookingDto>.Error(ServiceErrorMessages.Booking.AlreadyCancelledCannotAccept);

        if (booking.Status == BookingStatus.Accepted)
            return Result<BookingDto>.Error(ServiceErrorMessages.Booking.AlreadyAccepted);

        await _unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            booking.Status = BookingStatus.Accepted;

            await _unitOfWork.Repository<Booking>().UpdateAsync(booking, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await _unitOfWork.CommitTransactionAsync(cancellationToken);

            // Notify the user who made the booking
            await _notificationService.SendNotificationToUserAsync(
                Guid.Parse(booking.User!.Id),
                $"Your booking for {booking.Car!.Name?.Name} {booking.Car.Model?.Name} has been accepted.",
                UserNotificationType.BookingConfirmed);

            // Send email outside transaction — no need to roll back if email fails
            var emailResult = await _emailService.SendBookingAcceptanceEmailToUserAsync(
                booking.User!.Email!,
                $"{booking.User!.FirstName} {booking.User!.LastName}",
                $"{booking.Car!.Title}",
                $"{booking.Car.Name?.Name} {booking.Car.Model?.Name}",
                booking.StartDate.ToString("dd MMM yyyy"),
                booking.EndDate.ToString("dd MMM yyyy"),
                cancellationToken);

            if (!emailResult.IsSuccessful)
                _logger.LogWarning("Failed to send acceptance email to user for reference {Ref}", booking.Reference);

            var bookingDto = _mapper.Map<BookingDto>(booking);
            return Result<BookingDto>.Success(bookingDto);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            _logger.LogError(ex, "Failed to accept booking {BookingId}", request.BookingId);
            return Result<BookingDto>.Error(ServiceErrorMessages.Booking.AcceptFailed);
        }
    }


    public async Task<Result<BookingDto>> RefuseBookingRequestAsync(RefuseBookingCommand request, CancellationToken cancellationToken = default)
    {
        var booking = await _unitOfWork.Repository<Booking>()
            .AsQueryable()
            .Include(b => b.User)
            .Include(b => b.Car)
            .Include(b => b.Car!.Model)
            .Include(b => b.Car!.Name)
            .FirstOrDefaultAsync(p => p.Id.ToString() == request.BookingId, cancellationToken);

        if (booking is null)
            return Result<BookingDto>.Error(ServiceErrorMessages.Booking.NotFound);

        if (booking.Status == BookingStatus.Done)
            return Result<BookingDto>.Error(ServiceErrorMessages.Booking.AlreadyDoneCannotRefuse);

        if (booking.IsCanceled)
            return Result<BookingDto>.Error(ServiceErrorMessages.Booking.AlreadyCancelledCannotRefuse);

        if (booking.Status == BookingStatus.Refused)
            return Result<BookingDto>.Error(ServiceErrorMessages.Booking.AlreadyRefused);

        await _unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            booking.Status = BookingStatus.Refused;
            booking.RefuzedBy = RefuzedByType.Admin;
            booking.RefuzedReason = request.RefusedReanson;

            await _unitOfWork.Repository<Booking>().UpdateAsync(booking, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await _unitOfWork.CommitTransactionAsync(cancellationToken);

            // Notify the user who made the booking
            await _notificationService.SendNotificationToUserAsync(
                Guid.Parse(booking.User!.Id),
                $"Your booking for {booking.Car!.Name?.Name} {booking.Car.Model?.Name} has been refused.",
                UserNotificationType.BookingRejected);

            // Send email outside transaction — no need to roll back if email fails
            var emailResult = await _emailService.SendBookingRefusalEmailToUserAsync(
                booking.User!.Email!,
                $"{booking.User!.FirstName} {booking.User!.LastName}",
                $"{booking.Car!.Title}",
                $"{booking.Car.Name?.Name} {booking.Car.Model?.Name}",
                booking.RefuzedReason!,
                cancellationToken);

            if (!emailResult.IsSuccessful)
                _logger.LogWarning("Failed to send refusal email to user for reference {Ref}", booking.Reference);

            var bookingDto = _mapper.Map<BookingDto>(booking);
            return Result<BookingDto>.Success(bookingDto);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            _logger.LogError(ex, "Failed to refuse booking {BookingId}", request.BookingId);
            return Result<BookingDto>.Error(ServiceErrorMessages.Booking.RefuseFailed);
        }
    }

    public async Task<Result<BookingDto>> CloseBookingRequestAsync(CloseBookingCommand request, CancellationToken cancellationToken = default)
    {
        var booking = await _unitOfWork.Repository<Booking>()
            .AsQueryable()
            .Include(b => b.Car)
            .Include(b => b.Car!.Model)
            .Include(b => b.Car!.Name)
            .Include(b => b.User)
            .FirstOrDefaultAsync(p => p.Id.ToString() == request.BookingId, cancellationToken);

        if (booking is null)
            return Result<BookingDto>.Error(ServiceErrorMessages.Booking.NotFound);

        if (booking.IsCanceled)
            return Result<BookingDto>.Error(ServiceErrorMessages.Booking.AlreadyCancelledCannotClose);

        if (booking.Status == BookingStatus.Refused)
            return Result<BookingDto>.Error(ServiceErrorMessages.Booking.AlreadyRefusedCannotClose);

        if (booking.Status == BookingStatus.Done)
            return Result<BookingDto>.Error(ServiceErrorMessages.Booking.AlreadyDoneCannotClose);

        if (booking.Status != BookingStatus.Accepted)
            return Result<BookingDto>.Error(ServiceErrorMessages.Booking.MustBeAcceptedToClose);

        try
        {
            booking.Status = BookingStatus.Done;

            await _unitOfWork.Repository<Booking>().UpdateAsync(booking, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await _notificationService.SendNotificationToUserAsync(
                Guid.Parse(booking.User!.Id),
                $"Your booking for {booking.Car!.Name?.Name} {booking.Car.Model?.Name} has been completed.",
                UserNotificationType.BookingCompleted);

            await _notificationService.SendNotificationToAdminsAsync(
                $"Booking for {booking.Car!.Name?.Name} {booking.Car.Model?.Name} by {booking.User!.FirstName} {booking.User!.LastName} has been completed.",
                UserNotificationType.BookingCompleted);

            var bookingDto = _mapper.Map<BookingDto>(booking);
            return Result<BookingDto>.Success(bookingDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to close booking {BookingId}", request.BookingId);
            return Result<BookingDto>.Error(ServiceErrorMessages.Booking.CloseFailed);
        }
    }

    public async Task<Result<PagedResponse<BookingDto>>> GetAllBookingsAsync(GetAllBookingsQuery request, CancellationToken cancellationToken = default)
    {
        var query = _unitOfWork.Repository<Booking>()
            .AsQueryable()
            .Include(b => b.Car)
                .ThenInclude(c => c.CarImages)
            .Include(b => b.User)
                .ThenInclude(u => u.Image)
            .Include(b => b.BookingServices!)
            .ThenInclude(bs => bs.AdditionalService)
            .Where(c => !c.IsDeleted);

        // Apply filters
        if (!string.IsNullOrWhiteSpace(request.Search))
            query = query.Where(b =>
                b.Reference!.Contains(request.Search) ||
                b.User!.FirstName!.Contains(request.Search) ||
                b.User.LastName!.Contains(request.Search) ||
                b.User.UserName!.Contains(request.Search) ||
                b.Car!.Title!.Contains(request.Search) ||
                b.Car.Description!.Contains(request.Search));
        if (!string.IsNullOrWhiteSpace(request.Status))
            if (Enum.TryParse<BookingStatus>(request.Status, true, out var status))
                query = query.Where(b => b.Status == status);

        if (!string.IsNullOrWhiteSpace(request.PaymentType))
            if (Enum.TryParse<PaymentMethod>(request.PaymentType, true, out var paymentMethod))
                query = query.Where(b => b.PaymentMethod == paymentMethod);


        var totalCount = await query.CountAsync(cancellationToken);

        var bookings = await query
            .Skip((request.PageNr - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var mapped = _mapper.Map<List<BookingDto>>(bookings);

        var pagedResponse = new PagedResponse<BookingDto>(mapped, totalCount, request.PageNr, request.PageSize);

        return Result<PagedResponse<BookingDto>>.Success(pagedResponse);
    }


    private static string GenerateBookingReference()
    {
        const string prefix = "AZR";
        var year = DateTime.UtcNow.Year;
        var suffix = Random.Shared.Next(10000, 99999);

        return $"{prefix}-{year}-{suffix}";
    }

    public async Task<Result<IEnumerable<BookingDto>>> GetAllUserBookingsAsync(GetAllUserBookingsQuery request, CancellationToken cancellationToken = default)
    {
        var bookings = await _unitOfWork.Repository<Booking>()
           .AsQueryable()
            .Include(b => b.Car)
                .ThenInclude(c => c.CarImages)
            .Include(b => b.User)
                .ThenInclude(u => u.Image)
            .Include(b => b.BookingServices!)
                .ThenInclude(bs => bs.AdditionalService)
           .Where(b => b.User!.Id == request.UserId)
           .ToListAsync(cancellationToken);

        var mapped = _mapper.Map<IEnumerable<BookingDto>>(bookings);
        return Result.Success(mapped);
    }

}
