using AutoMapper;
using CarRentalZaimi.Application.Common.Errors;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Features.CarInterior.Commands.CreateCarInterior;
using CarRentalZaimi.Application.Features.CarInterior.Commands.DeleteCarInterior;
using CarRentalZaimi.Application.Features.CarInterior.Commands.UpdateCarInterior;
using CarRentalZaimi.Application.Features.CarInterior.Queries.GetAllCarInteriorColor;
using CarRentalZaimi.Application.Interfaces.Services;
using CarRentalZaimi.Application.Interfaces.UnitOfWork;
using CarRentalZaimi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRentalZaimi.Application.Services;

public class CarInteriorColorService(IUnitOfWork _unitOfWork, IMapper _mapper) : ICarInteriorColorService
{
    public async Task<ApiResponse<CarInteriorColorDto>> CreateAsync(CreateCarInteriorColorCommand request, CancellationToken cancellationToken = default)
    {
        var color = await _unitOfWork.Repository<CarInteriorColor>()
         .FirstOrDefaultAsync(p => p.Name == request.Name, cancellationToken);

        if (color is not null)
            return ApiResponse<CarInteriorColorDto>.FailureResponse(ErrorMessages.GetMessage(ErrorCodes.ALREADY_EXISTS));

        var newFuel = new CarInteriorColor
        {
            Name = request.Name!,
        };

        await _unitOfWork.Repository<CarInteriorColor>().AddAsync(newFuel, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse<CarInteriorColorDto>.SuccessResponse(_mapper.Map<CarInteriorColorDto>(newFuel));
    }

    public async Task<ApiResponse<bool>> DeleteAsync(DeleteCarInteriorColorCommand request, CancellationToken cancellationToken = default)
    {
        var existingColor = await _unitOfWork.Repository<CarInteriorColor>()
            .FirstOrDefaultAsync(p => p.Id.ToString() == request.Id, cancellationToken);

        if (existingColor is null)
            return ApiResponse<bool>.FailureResponse(ErrorMessages.GetMessage(ErrorCodes.NOT_FOUND));

        var isUsedByCar = await _unitOfWork.Repository<Car>()
            .AnyAsync(c => c.InteriorColorType != null && c.InteriorColorType.Id.ToString() == request.Id, cancellationToken);

        if (isUsedByCar)
            return ApiResponse<bool>.FailureResponse(ErrorMessages.GetMessage(ErrorCodes.INVALID_OPERATION));

        existingColor.IsDeleted = true;

        await _unitOfWork.Repository<CarInteriorColor>().UpdateAsync(existingColor, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse<bool>.SuccessResponse(true);
    }

    public async Task<ApiResponse<IEnumerable<CarInteriorColorDto>>> GetAllAsync(GetAllCarInteriorColorQuery request, CancellationToken cancellationToken = default)
    {
        var carColors = await _unitOfWork.Repository<CarInteriorColor>()
            .AsQueryable()
            .ToListAsync(cancellationToken);

        var mapped = _mapper.Map<IEnumerable<CarInteriorColorDto>>(carColors);
        return ApiResponse<IEnumerable<CarInteriorColorDto>>.SuccessResponse(mapped);
    }

    public async Task<ApiResponse<CarInteriorColorDto>> UpdateAsync(UpdateCarInteriorColorCommand request, CancellationToken cancellationToken = default)
    {
        var existingColor = await _unitOfWork.Repository<CarInteriorColor>()
           .FirstOrDefaultAsync(p => p.Id.ToString() == request.Id, cancellationToken);

        if (existingColor is null)
            return ApiResponse<CarInteriorColorDto>.FailureResponse(ErrorMessages.GetMessage(ErrorCodes.NOT_FOUND));

        var color = await _unitOfWork.Repository<CarInteriorColor>()
            .FirstOrDefaultAsync(p => p.Name == request.Name && p.Id.ToString() != request.Id, cancellationToken);

        if (color is not null)
            return ApiResponse<CarInteriorColorDto>.FailureResponse(ErrorMessages.GetMessage(ErrorCodes.ALREADY_EXISTS));

        existingColor.Name = request.Name!;

        await _unitOfWork.Repository<CarInteriorColor>().UpdateAsync(existingColor, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse<CarInteriorColorDto>.SuccessResponse(_mapper.Map<CarInteriorColorDto>(existingColor));
    }
}

