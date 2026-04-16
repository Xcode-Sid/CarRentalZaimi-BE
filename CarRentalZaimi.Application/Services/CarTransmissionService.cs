using AutoMapper;
using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.CarTransmission.Commands.CreateCarTransmission;
using CarRentalZaimi.Application.Features.CarTransmission.Commands.DeleteCarTransmission;
using CarRentalZaimi.Application.Features.CarTransmission.Commands.UpdateCarTransmission;
using CarRentalZaimi.Application.Features.CarTransmission.Queries.GetAllCarFuels;
using CarRentalZaimi.Application.Interfaces.Services;
using CarRentalZaimi.Application.Interfaces.UnitOfWork;
using CarRentalZaimi.Domain.Entities;
using CarRentalZaimi.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace CarRentalZaimi.Application.Services;

public class CarTransmissionService(IUnitOfWork _unitOfWork, IMapper _mapper, INotificationService _notificationService) : ICarTransmissionService
{
    public async Task<Result<CarTransmissionDto>> CreateAsync(CreateCarTransmissionCommand request, CancellationToken cancellationToken = default)
    {
        var transmission = await _unitOfWork.Repository<CarTransmission>()
          .FirstOrDefaultAsync(p => p.Name == request.Name, cancellationToken);

        if (transmission is not null)
            return Result<CarTransmissionDto>.Error("This car transmission type already exists");

        var newFuel = new CarTransmission
        {
            Name = request.Name!
        };

        await _unitOfWork.Repository<CarTransmission>().AddAsync(newFuel, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _notificationService.SendNotificationToAdminsAsync($"New car transmission type added: {newFuel.Name}", UserNotificationType.EntityAdded);

        return Result<CarTransmissionDto>.Success(_mapper.Map<CarTransmissionDto>(newFuel));
    }

    public async Task<Result<bool>> DeleteAsync(DeleteCarTransmissionCommand request, CancellationToken cancellationToken = default)
    {
        var existingTransmission = await _unitOfWork.Repository<CarTransmission>()
            .FirstOrDefaultAsync(p => p.Id.ToString() == request.Id, cancellationToken);

        if (existingTransmission is null)
            return Result<bool>.Error("This transmission type id does not exist");

        var isUsedByCar = await _unitOfWork.Repository<Car>()
            .AnyAsync(c => c.TransmissionType != null && c.TransmissionType.Id.ToString() == request.Id, cancellationToken);

        if (isUsedByCar)
            return Result<bool>.Error("This transmission type cannot be deleted because it is assigned to one or more cars");

        existingTransmission.IsDeleted = true;

        await _unitOfWork.Repository<CarTransmission>().UpdateAsync(existingTransmission, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _notificationService.SendNotificationToAdminsAsync($"Car transmission type deleted: {existingTransmission.Name}", UserNotificationType.EntityDeleted);

        return Result<bool>.Success(true);
    }

    public async Task<Result<IEnumerable<CarTransmissionDto>>> GetAllAsync(GetAllCarTransmissionQuery request, CancellationToken cancellationToken = default)
    {
        var carFuels = await _unitOfWork.Repository<CarTransmission>()
            .AsQueryable()
            .ToListAsync(cancellationToken);

        var mapped = _mapper.Map<IEnumerable<CarTransmissionDto>>(carFuels);
        return Result.Success(mapped);
    }

    public async Task<Result<CarTransmissionDto>> UpdateAsync(UpdateCarTransmissionCommand request, CancellationToken cancellationToken = default)
    {
        var existingTransmission = await _unitOfWork.Repository<CarTransmission>()
           .FirstOrDefaultAsync(p => p.Id.ToString() == request.Id, cancellationToken);

        if (existingTransmission is null)
            return Result<CarTransmissionDto>.Error("This transmission type id does not exists");

        var fuel = await _unitOfWork.Repository<CarTransmission>()
            .FirstOrDefaultAsync(p => p.Name == request.Name && p.Id.ToString() != request.Id, cancellationToken);

        if (fuel is not null)
            return Result<CarTransmissionDto>.Error("This transmission type already exists");

        existingTransmission.Name = request.Name!;

        await _unitOfWork.Repository<CarTransmission>().UpdateAsync(existingTransmission, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _notificationService.SendNotificationToAdminsAsync($"Car transmission type updated: {existingTransmission.Name}", UserNotificationType.EntityUpdated);

        return Result<CarTransmissionDto>.Success(_mapper.Map<CarTransmissionDto>(existingTransmission));
    }
}
