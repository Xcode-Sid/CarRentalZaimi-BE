using AutoMapper;
using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.Promotion.Commands.CreatePromotion;
using CarRentalZaimi.Application.Features.Promotion.Commands.DeletePromotion;
using CarRentalZaimi.Application.Features.Promotion.Commands.UpdatePromotion;
using CarRentalZaimi.Application.Features.Promotion.Queries.GetAllPromotion;
using CarRentalZaimi.Application.Features.Promotion.Queries.GetPromotionByCarId;
using CarRentalZaimi.Application.Interfaces.Services;
using CarRentalZaimi.Application.Interfaces.UnitOfWork;
using CarRentalZaimi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRentalZaimi.Application.Services;

public class PromotionService(IUnitOfWork _unitOfWork, IMapper _mapper, IEmailService _emailService) : IPromotionService
{
    public async Task<Result<PromotionDto>> CreateAsync(CreatePromotionCommand request, CancellationToken cancellationToken = default)
    {
        var existing = await _unitOfWork.Repository<Promotion>()
            .FirstOrDefaultAsync(p => p.Code == request.Code, cancellationToken);

        if (existing is not null)
            return Result<PromotionDto>.Error("A promotion with this code already exists.");

        bool hasCarId = !string.IsNullOrWhiteSpace(request.CarId);
        bool hasCategoryId = !string.IsNullOrWhiteSpace(request.CarCategoryId);

        if (hasCarId && hasCategoryId)
            return Result<PromotionDto>.Error("A promotion can target either a specific car or a category, not both.");

        Car? car = null;
        if (hasCarId)
        {
            car = await _unitOfWork.Repository<Car>()
                .FirstOrDefaultAsync(p => p.Id.ToString() == request.CarId, cancellationToken);

            if (car is null)
                return Result<PromotionDto>.Error("The specified car was not found.");
        }

        CarCategory? category = null;
        if (hasCategoryId)
        {
            category = await _unitOfWork.Repository<CarCategory>()
                .FirstOrDefaultAsync(p => p.Id.ToString() == request.CarCategoryId, cancellationToken);

            if (category is null)
                return Result<PromotionDto>.Error("The specified car category was not found.");
        }

        var newPromotion = new Promotion
        {
            Id                 = Guid.NewGuid(),
            Title              = request.Title,
            Description        = request.Description,
            Code               = request.Code,
            IsActive           = request.IsActive,
            DiscountPercentage = request.DiscountPercentage,
            NumberOfDays       = request.NumberOfDays,
            Car                = car,
            CarCategory        = category
        };

        await _unitOfWork.Repository<Promotion>().AddAsync(newPromotion, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Send emails AFTER save
        var subscribers = await _unitOfWork.Repository<Subscribe>().GetAllAsync(cancellationToken);
        var activeSubscribers = subscribers.Where(s => !s.IsUnsubscribed && s.Email != null);

        if (activeSubscribers.Any())
        {
            // Determine what the promotion applies to
            string appliesTo = (car, category) switch
            {
                ({ } c, _) => c.Title ?? "Specific Car",
                (_, { } cat) => $"{cat.Name} category",
                _ => "All Cars"
            };

            var emailTasks = activeSubscribers.Select(subscriber =>
                _emailService.SendNewPromotionNotificationEmailAsync(
                    subscriberEmail: subscriber.Email!,
                    title: newPromotion.Title,
                    code: newPromotion.Code,
                    discountPercentage: newPromotion.DiscountPercentage.ToString("F0"),
                    numberOfDays: newPromotion.NumberOfDays.ToString(),
                    appliesTo: appliesTo,
                    cancellationToken: cancellationToken
                )
            );

            await Task.WhenAll(emailTasks);
        }


        return Result<PromotionDto>.Success(_mapper.Map<PromotionDto>(newPromotion));
    }

    public async Task<Result<bool>> DeleteAsync(DeletePromotionCommand request, CancellationToken cancellationToken = default)
    {
        var existingCategory = await _unitOfWork.Repository<Promotion>()
        .FirstOrDefaultAsync(p => p.Id.ToString() == request.Id, cancellationToken);

        if (existingCategory is null)
            return Result<bool>.Error("This promotion id does not exist");

        existingCategory.IsDeleted = true;

        await _unitOfWork.Repository<Promotion>().UpdateAsync(existingCategory, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }

    public async Task<Result<PagedResponse<PromotionDto>>> GetAllAsync(GetAllPromotionQuery request, CancellationToken cancellationToken = default)
    {
        var query = _unitOfWork.Repository<Promotion>()
             .AsQueryable()
             .Where(c => !c.IsDeleted);

        // Search
        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.ToLower();
            query = query.Where(c =>
                (c.Title! != null && c.Title.ToLower().Contains(search)) ||
                (c.Description! != null && c.Description.ToLower().Contains(search)));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var promotions = await query
            .Skip((request.PageNr - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var mapped = _mapper.Map<List<PromotionDto>>(promotions);

        var pagedResponse = new PagedResponse<PromotionDto>(mapped, totalCount, request.PageNr, request.PageSize);

        return Result<PagedResponse<PromotionDto>>.Success(pagedResponse);
    }

    public async Task<Result<decimal>> GetPromotionByCarIdAsync(GetPromotionByCarIdQuery request, CancellationToken cancellationToken = default)
    {
        var promotion = await _unitOfWork.Repository<Promotion>()
            .AsQueryable()
            .Include(c => c.Car)
            .Include(c => c.CarCategory)
            .FirstOrDefaultAsync(c => c.Car.Id.ToString() == request.CarId
                                      && !c.IsDeleted
                                      && c.IsActive,cancellationToken);

        // 2️⃣ Fallback: look for an active promotion for the car's category
        if (promotion is null)
        {
            var car = await _unitOfWork.Repository<Car>()
                .AsQueryable()
                .FirstOrDefaultAsync(c => c.Id.ToString() == request.CarId && !c.IsDeleted, cancellationToken);

            if (car is null)
                return Result<decimal>.Error("Car not found.");

            promotion = await _unitOfWork.Repository<Promotion>()
                .AsQueryable()
                .Include(c => c.Car)
                .Include(c => c.CarCategory)
                .FirstOrDefaultAsync(c => c.CarCategory.Id == car.Category.Id
                                          && c.Car.Id == null          // category-level promotion (not tied to a specific car)
                                          && !c.IsDeleted
                                          && c.IsActive, cancellationToken);
        }

        if (promotion is null)
            return Result<decimal>.Error("No active promotion found for this car.");

        var promotionDto = _mapper.Map<PromotionDto>(promotion);

        return Result<decimal>.Success(promotion.DiscountPercentage);
    }

    public async Task<Result<PromotionDto>> UpdateAsync(UpdatePromotionCommand request, CancellationToken cancellationToken = default)
    {
        var existingCategory = await _unitOfWork.Repository<Promotion>()
           .FirstOrDefaultAsync(p => p.Id.ToString() == request.Id, cancellationToken);

        if (existingCategory is null)
            return Result<PromotionDto>.Error("This promotion id does not exists");

        var category = await _unitOfWork.Repository<Promotion>()
            .FirstOrDefaultAsync(p => p.Code == request.Code && p.Id.ToString() != request.Id, cancellationToken);

        if (category is not null)
            return Result<PromotionDto>.Error("This promotion already exists");

        existingCategory.Title = request.Title;
        existingCategory.Description = request.Description;
        existingCategory.IsActive = request.IsActive;
        existingCategory.NumberOfDays = request.NumberOfDays;
        existingCategory.DiscountPercentage = request.DiscountPercentage;

        await _unitOfWork.Repository<Promotion>().UpdateAsync(existingCategory, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<PromotionDto>.Success(_mapper.Map<PromotionDto>(existingCategory));
    }
}
