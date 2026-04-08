using AutoMapper;
using CarRentalZaimi.Application.Common;
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

namespace CarRentalZaimi.Application.Services;

public class CarReviewService(IUnitOfWork _unitOfWork, IMapper _mapper) : ICarReviewService
{
    public async Task<Result<CarReviewDto>> CreateCarReviewAsync(CreateCarReviewCommand request, CancellationToken cancellationToken = default)
    {
        var existingUser = await _unitOfWork.Repository<User>()
        .FirstOrDefaultAsync(p => p.Id == request.UserId, cancellationToken);

        if (existingUser is null)
            return Result<CarReviewDto>.Error("User not found");

        var existingCar = await _unitOfWork.Repository<Car>()
            .FirstOrDefaultAsync(p => p.Id.ToString() == request.CarId, cancellationToken);

        if (existingCar is null)
            return Result<CarReviewDto>.Error("Car not found");

        var existingReview = await _unitOfWork.Repository<CarReview>()
          .FirstOrDefaultAsync(p => p.User.Id == request.UserId && p.Post.Id.ToString() == request.CarId, cancellationToken);

        if (existingReview is not null)
            return Result<CarReviewDto>.Error("User can't add more than one review per car");

        
        var newReview = new CarReview
        {
            Id = Guid.NewGuid(),
            User = existingUser,
            Post = existingCar,
            Comment = request.Comment,
            Rating = request.Rating,
        };

        await _unitOfWork.Repository<CarReview>().AddAsync(newReview, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<CarReviewDto>.Success(_mapper.Map<CarReviewDto>(newReview));
    }

    public async Task<Result<bool>> DeleteCarReviewAsync(DeleteCarReviewCommand request, CancellationToken cancellationToken = default)
    {
        var existingReview = await _unitOfWork.Repository<CarReview>()
         .FirstOrDefaultAsync(p => p.Id.ToString() == request.Id, cancellationToken);

        if (existingReview is null)
            return Result<bool>.Error("Review not found");


        existingReview.IsDeleted = true;

        await _unitOfWork.Repository<CarReview>().UpdateAsync(existingReview, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

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
            return Result<CarReviewDto>.Error("Review not found");


        existingReview.Comment = request.Comment;
        existingReview.Rating = request.Rating;

        await _unitOfWork.Repository<CarReview>().UpdateAsync(existingReview, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<CarReviewDto>.Success(_mapper.Map<CarReviewDto>(existingReview));
    }
}
