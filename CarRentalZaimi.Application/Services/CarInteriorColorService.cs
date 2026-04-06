using AutoMapper;
using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
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
    public async Task<Result<CarInteriorColorDto>> CreateAsync(CreateCarInteriorColorCommand request, CancellationToken cancellationToken = default)
    {
        var color = await _unitOfWork.Repository<CarInteriorColor>()
         .FirstOrDefaultAsync(p => p.Name == request.Name, cancellationToken);

        if (color is not null)
            return Result<CarInteriorColorDto>.Error("This car interior color already exists");

        var newFuel = new CarInteriorColor
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
        };

        await _unitOfWork.Repository<CarInteriorColor>().AddAsync(newFuel, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<CarInteriorColorDto>.Success(_mapper.Map<CarInteriorColorDto>(newFuel));
    }

    public async Task<Result<bool>> DeleteAsync(DeleteCarInteriorColorCommand request, CancellationToken cancellationToken = default)
    {
        var existingColor = await _unitOfWork.Repository<CarInteriorColor>()
        .FirstOrDefaultAsync(p => p.Id.ToString() == request.Id, cancellationToken);

        if (existingColor is null)
            return Result<bool>.Error("This interior color id does not exist");

        existingColor.IsDeleted = true;

        await _unitOfWork.Repository<CarInteriorColor>().UpdateAsync(existingColor, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }

    public async Task<Result<IEnumerable<CarInteriorColorDto>>> GetAllAsync(GetAllCarInteriorColorQuery request, CancellationToken cancellationToken = default)
    {
        var carColors = await _unitOfWork.Repository<CarInteriorColor>()
            .AsQueryable()
            .ToListAsync(cancellationToken);

        var mapped = _mapper.Map<IEnumerable<CarInteriorColorDto>>(carColors);
        return Result.Success(mapped);
    }

    public async Task<Result<CarInteriorColorDto>> UpdateAsync(UpdateCarInteriorColorCommand request, CancellationToken cancellationToken = default)
    {
        var existingColor = await _unitOfWork.Repository<CarInteriorColor>()
           .FirstOrDefaultAsync(p => p.Id.ToString() == request.Id, cancellationToken);

        if (existingColor is null)
            return Result<CarInteriorColorDto>.Error("This interior color id does not exists");

        var color = await _unitOfWork.Repository<CarInteriorColor>()
            .FirstOrDefaultAsync(p => p.Name == request.Name && p.Id.ToString() != request.Id, cancellationToken);

        if (color is not null)
            return Result<CarInteriorColorDto>.Error("This interior color already exists");

        existingColor.Name = request.Name;

        await _unitOfWork.Repository<CarInteriorColor>().UpdateAsync(existingColor, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<CarInteriorColorDto>.Success(_mapper.Map<CarInteriorColorDto>(existingColor));
    }
}
