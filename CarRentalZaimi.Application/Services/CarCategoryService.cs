using AutoMapper;
using CarRentalZaimi.Application.Common.Errors;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Features.CarCategory.Command.CreateCarCategory;
using CarRentalZaimi.Application.Features.CarCategory.Command.DeleteCarCategory;
using CarRentalZaimi.Application.Features.CarCategory.Command.UpdateCarCategory;
using CarRentalZaimi.Application.Features.CarCategory.Queries;
using CarRentalZaimi.Application.Interfaces.Services;
using CarRentalZaimi.Application.Interfaces.UnitOfWork;
using CarRentalZaimi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRentalZaimi.Application.Services;

public class CarCategoryService(IUnitOfWork _unitOfWork, IMapper _mapper) : ICarCategoryService
{
    public async Task<ApiResponse<CarCategoryDto>> CreateAsync(CreateCarCategoryCommand request, CancellationToken cancellationToken = default)
    {
        var category = await _unitOfWork.Repository<CarCategory>()
        .FirstOrDefaultAsync(p => p.Name == request.Name, cancellationToken);

        if (category is not null)
            return ApiResponse<CarCategoryDto>.FailureResponse(ErrorMessages.GetMessage(ErrorCodes.ALREADY_EXISTS));

        var newFuel = new CarCategory
        {
            Name = request.Name!,
            Description = request.Description
        };

        await _unitOfWork.Repository<CarCategory>().AddAsync(newFuel, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse<CarCategoryDto>.SuccessResponse(_mapper.Map<CarCategoryDto>(newFuel));
    }

    public async Task<ApiResponse<bool>> DeleteAsync(DeleteCarCategoryCommand request, CancellationToken cancellationToken = default)
    {
        var existingCategory = await _unitOfWork.Repository<CarCategory>()
        .FirstOrDefaultAsync(p => p.Id.ToString() == request.Id, cancellationToken);

        if (existingCategory is null)
            return ApiResponse<bool>.FailureResponse(ErrorMessages.GetMessage(ErrorCodes.NOT_FOUND));

        var isUsedByCar = await _unitOfWork.Repository<Car>()
            .AnyAsync(c => c.Category != null && c.Category.Id.ToString() == request.Id, cancellationToken);

        if (isUsedByCar)
            return ApiResponse<bool>.FailureResponse(ErrorMessages.GetMessage(ErrorCodes.INVALID_OPERATION));

        existingCategory.IsDeleted = true;

        await _unitOfWork.Repository<CarCategory>().UpdateAsync(existingCategory, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse<bool>.SuccessResponse(true);
    }

    public async Task<ApiResponse<IEnumerable<CarCategoryDto>>> GetAllAsync(GetAllCarCategoryQuery request, CancellationToken cancellationToken = default)
    {
        var carColors = await _unitOfWork.Repository<CarCategory>()
           .AsQueryable()
           .ToListAsync(cancellationToken);

        var mapped = _mapper.Map<IEnumerable<CarCategoryDto>>(carColors);
        return ApiResponse<IEnumerable<CarCategoryDto>>.SuccessResponse(mapped);
    }

    public async Task<ApiResponse<CarCategoryDto>> UpdateAsync(UpdateCarCategoryCommand request, CancellationToken cancellationToken = default)
    {
        var existingCategory = await _unitOfWork.Repository<CarCategory>()
           .FirstOrDefaultAsync(p => p.Id.ToString() == request.Id, cancellationToken);

        if (existingCategory is null)
            return ApiResponse<CarCategoryDto>.FailureResponse(ErrorMessages.GetMessage(ErrorCodes.NOT_FOUND));

        var category = await _unitOfWork.Repository<CarCategory>()
            .FirstOrDefaultAsync(p => p.Name == request.Name && p.Id.ToString() != request.Id, cancellationToken);

        if (category is not null)
            return ApiResponse<CarCategoryDto>.FailureResponse(ErrorMessages.GetMessage(ErrorCodes.ALREADY_EXISTS));

        existingCategory.Name = request.Name!;
        existingCategory.Description = request.Description;

        await _unitOfWork.Repository<CarCategory>().UpdateAsync(existingCategory, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse<CarCategoryDto>.SuccessResponse(_mapper.Map<CarCategoryDto>(existingCategory));
    }
}

