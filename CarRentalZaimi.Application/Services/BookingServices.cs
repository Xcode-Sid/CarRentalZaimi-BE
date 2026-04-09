using AutoMapper;
using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.BookingRequest.Commands.CreateBookingRequest;
using CarRentalZaimi.Application.Features.BookingRequest.Queries.GetAllBookings;
using CarRentalZaimi.Application.Interfaces.Services;
using CarRentalZaimi.Application.Interfaces.UnitOfWork;
using CarRentalZaimi.Domain.Entities;
using CarRentalZaimi.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CarRentalZaimi.Application.Services;

public class BookingServices(IUnitOfWork _unitOfWork, IMapper _mapper) : IBookingServices
{
    public async Task<Result<BookingDto>> CreateBookingRequestAsync(CreateBookingRequestCommand request, CancellationToken cancellationToken = default)
    {
        var user = await _unitOfWork.Repository<User>()
            .FirstOrDefaultAsync(p => p.Id == request.UserId, cancellationToken);

        if (user is null)
            return Result<BookingDto>.Error("User not found");

        var car = await _unitOfWork.Repository<Car>()
            .FirstOrDefaultAsync(p => p.Id.ToString() == request.CarId, cancellationToken);

        if (car is null)
            return Result<BookingDto>.Error("Car not found");

        Enum.TryParse<PaymentMethod>(request.PaymentMethod, true, out var paymentMethod);

        var booking = new Booking
        {
            Id = Guid.NewGuid(),
            Reference = GenerateBookingReference(),
            PhoneNumber = request.PhoneNumber,
            PaymentMethod = paymentMethod,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Status = BookingStatus.Axcepted,
            TotalPrice = request.TotalPrice,
            User = user,
            Car = car,
        };

        foreach (var serviceId in request.AditionalServiceIds)
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

        return Result<BookingDto>.Success(_mapper.Map<BookingDto>(booking));
    }
    private static string GenerateBookingReference()
    {
        const string prefix = "AZR";
        var year = DateTime.UtcNow.Year;
        var suffix = Random.Shared.Next(10000, 99999);

        return $"{prefix}-{year}-{suffix}";
    }

    public async Task<Result<PagedResponse<BookingDto>>> GetAllBookingsAsync(GetAllBookingsQuery request, CancellationToken cancellationToken = default)
    {
        var query = _unitOfWork.Repository<Booking>()
            .AsQueryable()
            .Include(b => b.Car)
            .Include(b => b.User)
            .Include(b => b.BookingServices)
                .ThenInclude(bs => bs.AdditionalService) 
            .Where(c => !c.IsDeleted);

        // Apply filters
        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            query = query.Where(b =>
                b.Reference.Contains(request.Search) ||
                b.User.FirstName.Contains(request.Search) ||
                b.User.LastName.Contains(request.Search) ||
                b.User.UserName.Contains(request.Search) ||
                b.Car.Title.Contains(request.Search) ||
                b.Car.Description.Contains(request.Search));
        }
        if (!string.IsNullOrWhiteSpace(request.Status))
        {
            if (Enum.TryParse<BookingStatus>(request.Status, true, out var status))
                query = query.Where(b => b.Status == status);
        }

        if (!string.IsNullOrWhiteSpace(request.PaymentType))
        {
            if (Enum.TryParse<PaymentMethod>(request.PaymentType, true, out var paymentMethod))
                query = query.Where(b => b.PaymentMethod == paymentMethod);
        }


        var totalCount = await query.CountAsync(cancellationToken);

        var bookings = await query
            .Skip((request.PageNr - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var mapped = _mapper.Map<List<BookingDto>>(bookings);

        var pagedResponse = new PagedResponse<BookingDto>(mapped, totalCount, request.PageNr, request.PageSize);

        return Result<PagedResponse<BookingDto>>.Success(pagedResponse);
    }
}
