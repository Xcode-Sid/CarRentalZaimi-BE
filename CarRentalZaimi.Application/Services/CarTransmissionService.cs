using AutoMapper;
using CarRentalZaimi.Application.Common.Errors;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Features.CarTransmission.Commands.CreateCarTransmission;
using CarRentalZaimi.Application.Features.CarTransmission.Commands.DeleteCarTransmission;
using CarRentalZaimi.Application.Features.CarTransmission.Commands.UpdateCarTransmission;
using CarRentalZaimi.Application.Features.CarTransmission.Queries.GetAllCarFuels;
using CarRentalZaimi.Application.Interfaces.Services;
using CarRentalZaimi.Application.Interfaces.UnitOfWork;
using CarRentalZaimi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRentalZaimi.Application.Services;

public class CarTransmissionService(IUnitOfWork _unitOfWork, IMapper _mapper) : ICarTransmissionService
{
    public async Task<ApiResponse<CarTransmissionDto>> CreateAsync(CreateCarTransmissionCommand request, CancellationToken cancellationToken = default)
    {
        var transmission = await _unitOfWork.Repository<CarTransmission>()
          .FirstOrDefaultAsync(p => p.Name == request.Name, cancellationToken);

        if (transmission is not null)
            return ApiResponse<CarTransmissionDto>.FailureResponse(ErrorMessages.GetMessage(ErrorCodes.ALREADY_EXISTS));

        var newFuel = new CarTransmission
        {
            Name = request.Name!,
        };

        await _unitOfWork.Repository<CarTransmission>().AddAsync(newFuel, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse<CarTransmissionDto>.SuccessResponse(_mapper.Map<CarTransmissionDto>(newFuel));
    }

    public async Task<ApiResponse<bool>> DeleteAsync(DeleteCarTransmissionCommand request, CancellationToken cancellationToken = default)
    {
        var existingTransmission = await _unitOfWork.Repository<CarTransmission>()
            .FirstOrDefaultAsync(p => p.Id.ToString() == request.Id, cancellationToken);

        if (existingTransmission is null)
            return ApiResponse<bool>.FailureResponse(ErrorMessages.GetMessage(ErrorCodes.NOT_FOUND));

        var isUsedByCar = await _unitOfWork.Repository<Car>()
            .AnyAsync(c => c.TransmissionType != null && c.TransmissionType.Id.ToString() == request.Id, cancellationToken);

        if (isUsedByCar)
            return ApiResponse<bool>.FailureResponse(ErrorMessages.GetMessage(ErrorCodes.INVALID_OPERATION));

        existingTransmission.IsDeleted = true;

        await _unitOfWork.Repository<CarTransmission>().UpdateAsync(existingTransmission, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse<bool>.SuccessResponse(true);
    }

    public async Task<ApiResponse<IEnumerable<CarTransmissionDto>>> GetAllAsync(GetAllCarTransmissionQuery request, CancellationToken cancellationToken = default)
    {
        var carFuels = await _unitOfWork.Repository<CarTransmission>()
            .AsQueryable()
            .ToListAsync(cancellationToken);

        var mapped = _mapper.Map<IEnumerable<CarTransmissionDto>>(carFuels);
        return ApiResponse<IEnumerable<CarTransmissionDto>>.SuccessResponse(mapped);
    }

    public async Task<ApiResponse<CarTransmissionDto>> UpdateAsync(UpdateCarTransmissionCommand request, CancellationToken cancellationToken = default)
    {
        var existingTransmission = await _unitOfWork.Repository<CarTransmission>()
           .FirstOrDefaultAsync(p => p.Id.ToString() == request.Id, cancellationToken);

        if (existingTransmission is null)
            return ApiResponse<CarTransmissionDto>.FailureResponse(ErrorMessages.GetMessage(ErrorCodes.NOT_FOUND));

        var fuel = await _unitOfWork.Repository<CarTransmission>()
            .FirstOrDefaultAsync(p => p.Name == request.Name && p.Id.ToString() != request.Id, cancellationToken);

        if (fuel is not null)
            return ApiResponse<CarTransmissionDto>.FailureResponse(ErrorMessages.GetMessage(ErrorCodes.ALREADY_EXISTS));

        existingTransmission.Name = request.Name!;

        await _unitOfWork.Repository<CarTransmission>().UpdateAsync(existingTransmission, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse<CarTransmissionDto>.SuccessResponse(_mapper.Map<CarTransmissionDto>(existingTransmission));
    }
}

