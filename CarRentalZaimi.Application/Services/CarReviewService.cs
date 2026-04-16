using AutoMapper;
using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Common.Messages;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.CarReview.Commands.CreateCarReview;
using CarRentalZaimi.Application.Features.CarReview.Commands.DeleteCarReview;
using CarRentalZaimi.Application.Features.CarReview.Commands.UpdateCarReview;
using CarRentalZaimi.Application.Features.CarReview.Queries.GetAllCarReview;
using CarRentalZaimi.Application.Features.CarReview.Queries.GetAllPagedCarReview;
using CarRentalZaimi.Application.Interfaces.Services;
using CarRentalZaimi.Application.Interfaces.UnitOfWork;
using CarRentalZaimi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

using CarRentalZaimi.Domain.Enums;

namespace CarRentalZaimi.Application.Services;

public class CarReviewService(IUnitOfWork _unitOfWork, IMapper _mapper, INotificationService _notificationService) : ICarReviewService
{
    public async Task<Result<CarReviewDto>> CreateCarReviewAsync(CreateCarReviewCommand request, CancellationToken cancellationToken = default)
    {
        var existingUser = await _unitOfWork.Repository<User>()
        .FirstOrDefaultAsync(p => p.Id == request.UserId, cancellationToken);

        if (existingUser is null)
            return Result<CarReviewDto>.Error(ServiceErrorMessages.User.NotFound);

        var existingCar = await _unitOfWork.Repository<Car>()
            .FirstOrDefaultAsync(p => p.Id.ToString() == request.CarId, cancellationToken);

        if (existingCar is null)
            return Result<CarReviewDto>.Error(ServiceErrorMessages.Car.NotFound);

        var existingReview = await _unitOfWork.Repository<CarReview>()
          .FirstOrDefaultAsync(p => p.User.Id == request.UserId && p.Post.Id.ToString() == request.CarId, cancellationToken);

        if (existingReview is not null)
            return Result<CarReviewDto>.Error(ServiceErrorMessages.Review.DuplicateReview);

        
        var newReview = new CarReview
        {
            User = existingUser,
            Post = existingCar,
            Comment = request.Comment,
            Rating = request.Rating
        };

        await _unitOfWork.Repository<CarReview>().AddAsync(newReview, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Notify admins about new review
        await _notificationService.SendNotificationToAdminsAsync(
            $"New review for {existingCar.Title} by {existingUser.FirstName}: {newReview.Rating} stars.",
            UserNotificationType.NewReview);

        return Result<CarReviewDto>.Success(_mapper.Map<CarReviewDto>(newReview));
    }

    public async Task<Result<bool>> DeleteCarReviewAsync(DeleteCarReviewCommand request, CancellationToken cancellationToken = default)
    {
        var existingReview = await _unitOfWork.Repository<CarReview>()
         .FirstOrDefaultAsync(p => p.Id.ToString() == request.Id, cancellationToken);

        if (existingReview is null)
            return Result<bool>.Error(ServiceErrorMessages.Review.NotFound);


        existingReview.IsDeleted = true;

        await _unitOfWork.Repository<CarReview>().UpdateAsync(existingReview, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _notificationService.SendNotificationToAdminsAsync(
            $"Review deleted.",
            UserNotificationType.EntityDeleted);

        return Result<bool>.Success(true);
    }

    public async Task<Result<IEnumerable<CarReviewDto>>> GetAllAsync(GetAllCarReviewQuery request, CancellationToken cancellationToken = default)
    {
        var carFuels = await _unitOfWork.Repository<CarReview>()
         .AsQueryable()
         .Include(r => r.Post)
         .Include(r => r.User)
         .Where(r => r.Post.Id.ToString() == request.CarId)
         .ToListAsync(cancellationToken);

        var mapped = _mapper.Map<IEnumerable<CarReviewDto>>(carFuels);
        return Result.Success(mapped);
    }

    public async Task<Result<PagedResponse<CarReviewDto>>> GetAllPagedAsync(GetAllPagedCarReviewQuery request, CancellationToken cancellationToken = default)
    {
        var query = _unitOfWork.Repository<CarReview>()
            .AsQueryable()
            .Include(r => r.Post)
            .Include(r => r.User);

        var totalCount = await query.CountAsync(cancellationToken);

        var cars = await query
            .Skip((request.PageNr - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var mapped = _mapper.Map<List<CarReviewDto>>(cars);
        var pagedResponse = new PagedResponse<CarReviewDto>(mapped, totalCount, request.PageNr, request.PageSize);

        return Result<PagedResponse<CarReviewDto>>.Success(pagedResponse);
    }

    public async Task<Result<CarReviewDto>> UpdateCarReviewAsync(UpdateCarReviewCommand request, CancellationToken cancellationToken = default)
    {
       
        var existingReview = await _unitOfWork.Repository<CarReview>()
          .FirstOrDefaultAsync(p => p.Id.ToString() == request.Id, cancellationToken);

        if (existingReview is null)
            return Result<CarReviewDto>.Error(ServiceErrorMessages.Review.NotFound);


        existingReview.Comment = request.Comment;
        existingReview.Rating = request.Rating;

        await _unitOfWork.Repository<CarReview>().UpdateAsync(existingReview, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _notificationService.SendNotificationToAdminsAsync(
            $"Review updated: {existingReview.Rating} stars.",
            UserNotificationType.EntityUpdated);

        return Result<CarReviewDto>.Success(_mapper.Map<CarReviewDto>(existingReview));
    }
}
