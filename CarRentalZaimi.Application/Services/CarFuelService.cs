using AutoMapper;
using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
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
    public async Task<Result<CarFuelDto>> CreateAsync(CreateCarFuelCommand request, CancellationToken cancellationToken = default)
    {
        var fuel = await _unitOfWork.Repository<CarFuel>()
            .FirstOrDefaultAsync(p => p.Name == request.Name, cancellationToken);

        if (fuel is not null)
            return Result<CarFuelDto>.Error("This car fuel type already exists");

        var newFuel = new CarFuel
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
        };

        await _unitOfWork.Repository<CarFuel>().AddAsync(newFuel, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<CarFuelDto>.Success(_mapper.Map<CarFuelDto>(newFuel));
    }

    public async Task<Result<bool>> DeleteAsync(DeleteCarFuelCommand request, CancellationToken cancellationToken = default)
    {
        var existingFuel = await _unitOfWork.Repository<CarFuel>()
            .FirstOrDefaultAsync(p => p.Id.ToString() == request.Id, cancellationToken);

        if (existingFuel is null)
            return Result<bool>.Error("This fuel type id does not exist");

        var isUsedByCar = await _unitOfWork.Repository<Car>()
            .AnyAsync(c => c.FuelType != null && c.FuelType.Id.ToString() == request.Id, cancellationToken);

        if (isUsedByCar)
            return Result<bool>.Error("This fuel type cannot be deleted because it is assigned to one or more cars");

        existingFuel.IsDeleted = true;

        await _unitOfWork.Repository<CarFuel>().UpdateAsync(existingFuel, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }

    public async Task<Result<IEnumerable<CarFuelDto>>> GetAllAsync(GetAllCarFuelsQuery request, CancellationToken cancellationToken = default)
    {
        var carFuels= await _unitOfWork.Repository<CarFuel>()
          .AsQueryable()
          .ToListAsync(cancellationToken);

        var mapped = _mapper.Map<IEnumerable<CarFuelDto>>(carFuels);
        return Result.Success(mapped);
    }

    public async Task<Result<CarFuelDto>> UpdateAsync(UpdateCarFuelCommand request, CancellationToken cancellationToken = default)
    {
        var existingFuel = await _unitOfWork.Repository<CarFuel>()
           .FirstOrDefaultAsync(p => p.Id.ToString() == request.Id, cancellationToken);

        if (existingFuel is null)
            return Result<CarFuelDto>.Error("This fuel type id does not exists");

        var fuel = await _unitOfWork.Repository<CarFuel>()
            .FirstOrDefaultAsync(p => p.Name == request.Name && p.Id.ToString() != request.Id, cancellationToken);

        if (fuel is not null)
            return Result<CarFuelDto>.Error("This fuel type already exists");

        existingFuel.Name = request.Name;

        await _unitOfWork.Repository<CarFuel>().UpdateAsync(existingFuel, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<CarFuelDto>.Success(_mapper.Map<CarFuelDto>(existingFuel));
    }
}
