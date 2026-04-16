using AutoMapper;
using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Common.Messages;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.Promotion.Commands.CreatePromotion;
using CarRentalZaimi.Application.Features.Promotion.Commands.DeletePromotion;
using CarRentalZaimi.Application.Features.Promotion.Commands.UpdatePromotion;
using CarRentalZaimi.Application.Features.Promotion.Queries.GetAllPromotion;
using CarRentalZaimi.Application.Features.Promotion.Queries.GetPromotionByCarId;
using CarRentalZaimi.Application.Interfaces.Services;
using CarRentalZaimi.Application.Interfaces.UnitOfWork;
using CarRentalZaimi.Domain.Entities;
using CarRentalZaimi.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace CarRentalZaimi.Application.Services;

public class PromotionService(IUnitOfWork _unitOfWork, IMapper _mapper, IEmailService _emailService, INotificationService _notificationService) : IPromotionService
{
    public async Task<Result<PromotionDto>> CreateAsync(CreatePromotionCommand request, CancellationToken cancellationToken = default)
    {
        var existing = await _unitOfWork.Repository<Promotion>()
            .FirstOrDefaultAsync(p => p.Code == request.Code, cancellationToken);

        if (existing is not null)
            return Result<PromotionDto>.Error(ServiceErrorMessages.Promotion.CodeAlreadyExists);

        bool hasCarId = !string.IsNullOrWhiteSpace(request.CarId);
        bool hasCategoryId = !string.IsNullOrWhiteSpace(request.CarCategoryId);

        if (hasCarId && hasCategoryId)
            return Result<PromotionDto>.Error(ServiceErrorMessages.Promotion.CannotTargetBoth);

        Car? car = null;
        if (hasCarId)
        {
            car = await _unitOfWork.Repository<Car>()
                .FirstOrDefaultAsync(p => p.Id.ToString() == request.CarId, cancellationToken);

            if (car is null)
                return Result<PromotionDto>.Error(ServiceErrorMessages.Promotion.CarNotFound);
        }

        CarCategory? category = null;
        if (hasCategoryId)
        {
            category = await _unitOfWork.Repository<CarCategory>()
                .FirstOrDefaultAsync(p => p.Id.ToString() == request.CarCategoryId, cancellationToken);

            if (category is null)
                return Result<PromotionDto>.Error(ServiceErrorMessages.Promotion.CategoryNotFound);
        }

        var newPromotion = new Promotion
        {
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

        // Notify all users about new promotion
        await _notificationService.SendNotificationToAllAsync(
            string.Format(NotificationMessages.Promotion.Created, newPromotion.Title, newPromotion.Code, newPromotion.DiscountPercentage),
            UserNotificationType.NewPromotion);

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
                    title: newPromotion.Title!,
                    code: newPromotion.Code!,
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
            return Result<bool>.Error(ServiceErrorMessages.Promotion.NotFound);

        existingCategory.IsDeleted = true;

        await _unitOfWork.Repository<Promotion>().UpdateAsync(existingCategory, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _notificationService.SendNotificationToAdminsAsync(
            string.Format(NotificationMessages.Promotion.Deleted, existingCategory.Title),
            UserNotificationType.EntityDeleted);

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
        // Parse once, outside the query
        if (!Guid.TryParse(request.CarId, out var carGuid))
            return Result<decimal>.Error(ServiceErrorMessages.Promotion.InvalidCarId);

        // 1️⃣ Try to find a promotion tied directly to this car
        var promotion = await _unitOfWork.Repository<Promotion>()
            .AsQueryable()
            .Include(c => c.Car)
            .Include(c => c.CarCategory)
            .FirstOrDefaultAsync(c => c.Car!.Id == carGuid
                                      && !c.IsDeleted
                                      && c.IsActive, cancellationToken);

        // 2️⃣ Fallback: look for an active promotion for the car's category
        if (promotion is null)
        {
            var car = await _unitOfWork.Repository<Car>()
                .AsQueryable()
                .Include(c => c.Category)
                .FirstOrDefaultAsync(c => c.Id == carGuid && !c.IsDeleted, cancellationToken);

            if (car is null)
                return Result<decimal>.Error(ServiceErrorMessages.Car.NotFound);

            var categoryId = car.Category!.Id; 

            promotion = await _unitOfWork.Repository<Promotion>()
                .AsQueryable()
                .Include(c => c.Car)
                .Include(c => c.CarCategory)
                .FirstOrDefaultAsync(c => c.CarCategory!.Id == categoryId
                                          && !c.IsDeleted
                                          && c.IsActive, cancellationToken);
        }

        if (promotion is null)
            return Result<decimal>.Error(ServiceErrorMessages.Promotion.NoActivePromotion);

        return Result<decimal>.Success(promotion.DiscountPercentage);
    }

    public async Task<Result<PromotionDto>> UpdateAsync(UpdatePromotionCommand request, CancellationToken cancellationToken = default)
    {
        var existingCategory = await _unitOfWork.Repository<Promotion>()
           .FirstOrDefaultAsync(p => p.Id.ToString() == request.Id, cancellationToken);

        if (existingCategory is null)
            return Result<PromotionDto>.Error(ServiceErrorMessages.Promotion.NotFoundById);

        var category = await _unitOfWork.Repository<Promotion>()
            .FirstOrDefaultAsync(p => p.Code == request.Code && p.Id.ToString() != request.Id, cancellationToken);

        if (category is not null)
            return Result<PromotionDto>.Error(ServiceErrorMessages.Promotion.DuplicateCode);

        existingCategory.Title = request.Title;
        existingCategory.Description = request.Description;
        existingCategory.IsActive = request.IsActive;
        existingCategory.NumberOfDays = request.NumberOfDays;
        existingCategory.DiscountPercentage = request.DiscountPercentage;

        await _unitOfWork.Repository<Promotion>().UpdateAsync(existingCategory, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _notificationService.SendNotificationToAdminsAsync(
            string.Format(NotificationMessages.Promotion.Updated, existingCategory.Title),
            UserNotificationType.EntityUpdated);

        return Result<PromotionDto>.Success(_mapper.Map<PromotionDto>(existingCategory));
    }
}
