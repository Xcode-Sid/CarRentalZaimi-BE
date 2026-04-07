using AutoMapper;
using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.CarExteriorColor.Commands.CreateCarExteriorColor;
using CarRentalZaimi.Application.Features.CarExteriorColor.Commands.DeleteCarExteriorColor;
using CarRentalZaimi.Application.Features.CarExteriorColor.Commands.UpdateCarExteriorColor;
using CarRentalZaimi.Application.Features.CarExteriorColor.Queries;
using CarRentalZaimi.Application.Features.CarInterior.Queries.GetAllCarInteriorColor;
using CarRentalZaimi.Application.Interfaces.Services;
using CarRentalZaimi.Application.Interfaces.UnitOfWork;
using CarRentalZaimi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRentalZaimi.Application.Services;

public class CarExteriorColorService(IUnitOfWork _unitOfWork, IMapper _mapper) : ICarExteriorColorService
{
    public async Task<Result<CarExteriorColorDto>> CreateAsync(CreateCarExteriorColorCommand request, CancellationToken cancellationToken = default)
    {
        var color = await _unitOfWork.Repository<CarExteriorColor>()
        .FirstOrDefaultAsync(p => p.Name == request.Name, cancellationToken);

        if (color is not null)
            return Result<CarExteriorColorDto>.Error("This car exterior color already exists");

        var newFuel = new CarExteriorColor
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
        };

        await _unitOfWork.Repository<CarExteriorColor>().AddAsync(newFuel, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<CarExteriorColorDto>.Success(_mapper.Map<CarExteriorColorDto>(newFuel));
    }

    public async Task<Result<bool>> DeleteAsync(DeleteCarExteriorColorCommand request, CancellationToken cancellationToken = default)
    {
        var existingColor = await _unitOfWork.Repository<CarExteriorColor>()
            .FirstOrDefaultAsync(p => p.Id.ToString() == request.Id, cancellationToken);

        if (existingColor is null)
            return Result<bool>.Error("This exterior color id does not exist");

        var isUsedByCar = await _unitOfWork.Repository<Car>()
            .AnyAsync(c => c.ExteriorColorType != null && c.ExteriorColorType.Id.ToString() == request.Id, cancellationToken);

        if (isUsedByCar)
            return Result<bool>.Error("This exterior color cannot be deleted because it is assigned to one or more cars");

        existingColor.IsDeleted = true;

        await _unitOfWork.Repository<CarExteriorColor>().UpdateAsync(existingColor, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }

    public async Task<Result<IEnumerable<CarExteriorColorDto>>> GetAllAsync(GetAllCarExteriorColorQuery request, CancellationToken cancellationToken = default)
    {
        var carColors = await _unitOfWork.Repository<CarExteriorColor>()
            .AsQueryable()
            .ToListAsync(cancellationToken);

        var mapped = _mapper.Map<IEnumerable<CarExteriorColorDto>>(carColors);
        return Result.Success(mapped);
    }

    public async Task<Result<CarExteriorColorDto>> UpdateAsync(UpdateCarExteriorColorCommand request, CancellationToken cancellationToken = default)
    {
        var existingColor = await _unitOfWork.Repository<CarExteriorColor>()
          .FirstOrDefaultAsync(p => p.Id.ToString() == request.Id, cancellationToken);

        if (existingColor is null)
            return Result<CarExteriorColorDto>.Error("This exterior color id does not exists");

        var color = await _unitOfWork.Repository<CarExteriorColor>()
            .FirstOrDefaultAsync(p => p.Name == request.Name && p.Id.ToString() != request.Id, cancellationToken);

        if (color is not null)
            return Result<CarExteriorColorDto>.Error("This exterior color already exists");

        existingColor.Name = request.Name;

        await _unitOfWork.Repository<CarExteriorColor>().UpdateAsync(existingColor, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<CarExteriorColorDto>.Success(_mapper.Map<CarExteriorColorDto>(existingColor));
    }
}
