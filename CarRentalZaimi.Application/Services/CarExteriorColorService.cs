using AutoMapper;
using CarRentalZaimi.Application.Common.Errors;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Features.CarExteriorColor.Commands.CreateCarExteriorColor;
using CarRentalZaimi.Application.Features.CarExteriorColor.Commands.DeleteCarExteriorColor;
using CarRentalZaimi.Application.Features.CarExteriorColor.Commands.UpdateCarExteriorColor;
using CarRentalZaimi.Application.Features.CarExteriorColor.Queries;
using CarRentalZaimi.Application.Interfaces.Services;
using CarRentalZaimi.Application.Interfaces.UnitOfWork;
using CarRentalZaimi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRentalZaimi.Application.Services;

public class CarExteriorColorService(IUnitOfWork _unitOfWork, IMapper _mapper) : ICarExteriorColorService
{
    public async Task<ApiResponse<CarExteriorColorDto>> CreateAsync(CreateCarExteriorColorCommand request, CancellationToken cancellationToken = default)
    {
        var color = await _unitOfWork.Repository<CarExteriorColor>()
        .FirstOrDefaultAsync(p => p.Name == request.Name, cancellationToken);

        if (color is not null)
            return ApiResponse<CarExteriorColorDto>.FailureResponse(ErrorMessages.GetMessage(ErrorCodes.ALREADY_EXISTS));

        var newFuel = new CarExteriorColor
        {
            Name = request.Name!,
        };

        await _unitOfWork.Repository<CarExteriorColor>().AddAsync(newFuel, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse<CarExteriorColorDto>.SuccessResponse(_mapper.Map<CarExteriorColorDto>(newFuel));
    }

    public async Task<ApiResponse<bool>> DeleteAsync(DeleteCarExteriorColorCommand request, CancellationToken cancellationToken = default)
    {
        var existingColor = await _unitOfWork.Repository<CarExteriorColor>()
            .FirstOrDefaultAsync(p => p.Id.ToString() == request.Id, cancellationToken);

        if (existingColor is null)
            return ApiResponse<bool>.FailureResponse(ErrorMessages.GetMessage(ErrorCodes.NOT_FOUND));

        var isUsedByCar = await _unitOfWork.Repository<Car>()
            .AnyAsync(c => c.ExteriorColorType != null && c.ExteriorColorType.Id.ToString() == request.Id, cancellationToken);

        if (isUsedByCar)
            return ApiResponse<bool>.FailureResponse(ErrorMessages.GetMessage(ErrorCodes.INVALID_OPERATION));

        existingColor.IsDeleted = true;

        await _unitOfWork.Repository<CarExteriorColor>().UpdateAsync(existingColor, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse<bool>.SuccessResponse(true);
    }

    public async Task<ApiResponse<IEnumerable<CarExteriorColorDto>>> GetAllAsync(GetAllCarExteriorColorQuery request, CancellationToken cancellationToken = default)
    {
        var carColors = await _unitOfWork.Repository<CarExteriorColor>()
            .AsQueryable()
            .ToListAsync(cancellationToken);

        var mapped = _mapper.Map<IEnumerable<CarExteriorColorDto>>(carColors);
        return ApiResponse<IEnumerable<CarExteriorColorDto>>.SuccessResponse(mapped);
    }

    public async Task<ApiResponse<CarExteriorColorDto>> UpdateAsync(UpdateCarExteriorColorCommand request, CancellationToken cancellationToken = default)
    {
        var existingColor = await _unitOfWork.Repository<CarExteriorColor>()
          .FirstOrDefaultAsync(p => p.Id.ToString() == request.Id, cancellationToken);

        if (existingColor is null)
            return ApiResponse<CarExteriorColorDto>.FailureResponse(ErrorMessages.GetMessage(ErrorCodes.NOT_FOUND));

        var color = await _unitOfWork.Repository<CarExteriorColor>()
            .FirstOrDefaultAsync(p => p.Name == request.Name && p.Id.ToString() != request.Id, cancellationToken);

        if (color is not null)
            return ApiResponse<CarExteriorColorDto>.FailureResponse(ErrorMessages.GetMessage(ErrorCodes.ALREADY_EXISTS));

        existingColor.Name = request.Name!;

        await _unitOfWork.Repository<CarExteriorColor>().UpdateAsync(existingColor, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse<CarExteriorColorDto>.SuccessResponse(_mapper.Map<CarExteriorColorDto>(existingColor));
    }
}

