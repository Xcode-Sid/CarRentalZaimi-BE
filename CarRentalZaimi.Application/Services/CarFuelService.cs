using AutoMapper;
using CarRentalZaimi.Application.Common.Errors;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Features.CarFuel.Commands.CreateCarFuel;
using CarRentalZaimi.Application.Features.CarFuel.Commands.DeleteCarFuel;
using CarRentalZaimi.Application.Features.CarFuel.Commands.UpdateCarFuel;
using CarRentalZaimi.Application.Features.CarFuel.Queries.GetAllCarFuels;
using CarRentalZaimi.Application.Interfaces.Services;
using CarRentalZaimi.Application.Interfaces.UnitOfWork;
using CarRentalZaimi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRentalZaimi.Application.Services;

public class CarFuelService(IUnitOfWork _unitOfWork, IMapper _mapper) : ICarFuelService
{
    public async Task<ApiResponse<CarFuelDto>> CreateAsync(CreateCarFuelCommand request, CancellationToken cancellationToken = default)
    {
        var fuel = await _unitOfWork.Repository<CarFuel>()
            .FirstOrDefaultAsync(p => p.Name == request.Name, cancellationToken);

        if (fuel is not null)
            return ApiResponse<CarFuelDto>.FailureResponse(ErrorMessages.GetMessage(ErrorCodes.ALREADY_EXISTS));

        var newFuel = new CarFuel
        {
            Name = request.Name!,
        };

        await _unitOfWork.Repository<CarFuel>().AddAsync(newFuel, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse<CarFuelDto>.SuccessResponse(_mapper.Map<CarFuelDto>(newFuel));
    }

    public async Task<ApiResponse<bool>> DeleteAsync(DeleteCarFuelCommand request, CancellationToken cancellationToken = default)
    {
        var existingFuel = await _unitOfWork.Repository<CarFuel>()
            .FirstOrDefaultAsync(p => p.Id.ToString() == request.Id, cancellationToken);

        if (existingFuel is null)
            return ApiResponse<bool>.FailureResponse(ErrorMessages.GetMessage(ErrorCodes.NOT_FOUND));

        var isUsedByCar = await _unitOfWork.Repository<Car>()
            .AnyAsync(c => c.FuelType != null && c.FuelType.Id.ToString() == request.Id, cancellationToken);

        if (isUsedByCar)
            return ApiResponse<bool>.FailureResponse(ErrorMessages.GetMessage(ErrorCodes.INVALID_OPERATION));

        existingFuel.IsDeleted = true;

        await _unitOfWork.Repository<CarFuel>().UpdateAsync(existingFuel, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse<bool>.SuccessResponse(true);
    }

    public async Task<ApiResponse<IEnumerable<CarFuelDto>>> GetAllAsync(GetAllCarFuelsQuery request, CancellationToken cancellationToken = default)
    {
        var carFuels= await _unitOfWork.Repository<CarFuel>()
          .AsQueryable()
          .ToListAsync(cancellationToken);

        var mapped = _mapper.Map<IEnumerable<CarFuelDto>>(carFuels);
        return ApiResponse<IEnumerable<CarFuelDto>>.SuccessResponse(mapped);
    }

    public async Task<ApiResponse<CarFuelDto>> UpdateAsync(UpdateCarFuelCommand request, CancellationToken cancellationToken = default)
    {
        var existingFuel = await _unitOfWork.Repository<CarFuel>()
           .FirstOrDefaultAsync(p => p.Id.ToString() == request.Id, cancellationToken);

        if (existingFuel is null)
            return ApiResponse<CarFuelDto>.FailureResponse(ErrorMessages.GetMessage(ErrorCodes.NOT_FOUND));

        var fuel = await _unitOfWork.Repository<CarFuel>()
            .FirstOrDefaultAsync(p => p.Name == request.Name && p.Id.ToString() != request.Id, cancellationToken);

        if (fuel is not null)
            return ApiResponse<CarFuelDto>.FailureResponse(ErrorMessages.GetMessage(ErrorCodes.ALREADY_EXISTS));

        existingFuel.Name = request.Name!;

        await _unitOfWork.Repository<CarFuel>().UpdateAsync(existingFuel, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse<CarFuelDto>.SuccessResponse(_mapper.Map<CarFuelDto>(existingFuel));
    }
}

