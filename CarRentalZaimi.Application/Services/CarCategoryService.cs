using AutoMapper;
using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Common.Messages;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.CarCategory.Command.CreateCarCategory;
using CarRentalZaimi.Application.Features.CarCategory.Command.DeleteCarCategory;
using CarRentalZaimi.Application.Features.CarCategory.Command.UpdateCarCategory;
using CarRentalZaimi.Application.Features.CarCategory.Queries;
using CarRentalZaimi.Application.Interfaces.Services;
using CarRentalZaimi.Application.Interfaces.UnitOfWork;
using CarRentalZaimi.Domain.Entities;
using CarRentalZaimi.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace CarRentalZaimi.Application.Services;

public class CarCategoryService(IUnitOfWork _unitOfWork, IMapper _mapper, INotificationService _notificationService) : ICarCategoryService
{
    public async Task<Result<CarCategoryDto>> CreateAsync(CreateCarCategoryCommand request, CancellationToken cancellationToken = default)
    {
        var category = await _unitOfWork.Repository<CarCategory>()
        .FirstOrDefaultAsync(p => p.Name == request.Name, cancellationToken);

        if (category is not null)
            return Result<CarCategoryDto>.Error("This car category already exists");

        var newFuel = new CarCategory
        {
            Name = request.Name!,
            Description = request.Description
        };

        await _unitOfWork.Repository<CarCategory>().AddAsync(newFuel, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _notificationService.SendNotificationToAdminsAsync($"New car category added: {newFuel.Name}", UserNotificationType.EntityAdded);

        return Result<CarCategoryDto>.Success(_mapper.Map<CarCategoryDto>(newFuel));
    }

    public async Task<Result<bool>> DeleteAsync(DeleteCarCategoryCommand request, CancellationToken cancellationToken = default)
    {
        var existingCategory = await _unitOfWork.Repository<CarCategory>()
        .FirstOrDefaultAsync(p => p.Id.ToString() == request.Id, cancellationToken);

        if (existingCategory is null)
            return Result<bool>.Error("This category id does not exist");

        var isUsedByCar = await _unitOfWork.Repository<Car>()
            .AnyAsync(c => c.Category != null && c.Category.Id.ToString() == request.Id, cancellationToken);

        if (isUsedByCar)
            return Result<bool>.Error("This category cannot be deleted because it is assigned to one or more cars");

        existingCategory.IsDeleted = true;

        await _unitOfWork.Repository<CarCategory>().UpdateAsync(existingCategory, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _notificationService.SendNotificationToAdminsAsync($"Car category deleted: {existingCategory.Name}", UserNotificationType.EntityDeleted);

        return Result<bool>.Success(true);
    }

    public async Task<Result<IEnumerable<CarCategoryDto>>> GetAllAsync(GetAllCarCategoryQuery request, CancellationToken cancellationToken = default)
    {
        var carColors = await _unitOfWork.Repository<CarCategory>()
           .AsQueryable()
           .ToListAsync(cancellationToken);

        var mapped = _mapper.Map<IEnumerable<CarCategoryDto>>(carColors);
        return Result.Success(mapped);
    }

    public async Task<Result<CarCategoryDto>> UpdateAsync(UpdateCarCategoryCommand request, CancellationToken cancellationToken = default)
    {
        var existingCategory = await _unitOfWork.Repository<CarCategory>()
           .FirstOrDefaultAsync(p => p.Id.ToString() == request.Id, cancellationToken);

        if (existingCategory is null)
            return Result<CarCategoryDto>.Error("This category id does not exists");

        var category = await _unitOfWork.Repository<CarCategory>()
            .FirstOrDefaultAsync(p => p.Name == request.Name && p.Id.ToString() != request.Id, cancellationToken);

        if (category is not null)
            return Result<CarCategoryDto>.Error("This category already exists");

        existingCategory.Name = request.Name!;
        existingCategory.Description = request.Description;

        await _unitOfWork.Repository<CarCategory>().UpdateAsync(existingCategory, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _notificationService.SendNotificationToAdminsAsync($"Car category updated: {existingCategory.Name}", UserNotificationType.EntityUpdated);

        return Result<CarCategoryDto>.Success(_mapper.Map<CarCategoryDto>(existingCategory));
    }
}
