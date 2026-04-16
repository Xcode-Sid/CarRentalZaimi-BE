using AutoMapper;
using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.CarCompanyName.Commands.CreateCarCompanyName;
using CarRentalZaimi.Application.Features.CarCompanyName.Commands.DeleteCarCompanyName;
using CarRentalZaimi.Application.Features.CarCompanyName.Commands.UpdateCarCompanyName;
using CarRentalZaimi.Application.Features.CarCompanyName.Queries.GetAllCarCompanyName;
using CarRentalZaimi.Application.Interfaces.Services;
using CarRentalZaimi.Application.Interfaces.UnitOfWork;
using CarRentalZaimi.Domain.Entities;
using CarRentalZaimi.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace CarRentalZaimi.Application.Services;

public class CarCompanyNameService(IUnitOfWork _unitOfWork, IMapper _mapper, INotificationService _notificationService) : ICarCompanyNameService
{
    public async Task<Result<CarCompanyNameDto>> CreateAsync(CreateCarCompanyNameCommand request, CancellationToken cancellationToken = default)
    {
        var company = await _unitOfWork.Repository<CarCompanyName>()
         .FirstOrDefaultAsync(p => p.Name == request.Name, cancellationToken);

        if (company is not null)
            return Result<CarCompanyNameDto>.Error("This car company name already exists");

        var newFuel = new CarCompanyName
        {
            Name = request.Name!
        };

        await _unitOfWork.Repository<CarCompanyName>().AddAsync(newFuel, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _notificationService.SendNotificationToAdminsAsync($"New car company added: {newFuel.Name}", UserNotificationType.EntityAdded);

        return Result<CarCompanyNameDto>.Success(_mapper.Map<CarCompanyNameDto>(newFuel));
    }

    public async Task<Result<bool>> DeleteAsync(DeleteCarCompanyNameCommand request, CancellationToken cancellationToken = default)
    {
        var existingCompany = await _unitOfWork.Repository<CarCompanyName>()
            .FirstOrDefaultAsync(p => p.Id.ToString() == request.Id, cancellationToken);

        if (existingCompany is null)
            return Result<bool>.Error("This car company id does not exist");

        var isUsedByModel = await _unitOfWork.Repository<CarCompanyModel>()
            .AnyAsync(m => m.CarCompanyName != null && m.CarCompanyName.Id.ToString() == request.Id, cancellationToken);

        if (isUsedByModel)
            return Result<bool>.Error("This car company cannot be deleted because it is assigned to one or more car models");

        var isUsedByCar = await _unitOfWork.Repository<Car>()
            .AnyAsync(c => c.Name != null && c.Name.Id.ToString() == request.Id, cancellationToken);

        if (isUsedByCar)
            return Result<bool>.Error("This car company cannot be deleted because it is assigned to one or more cars");

        existingCompany.IsDeleted = true;

        await _unitOfWork.Repository<CarCompanyName>().UpdateAsync(existingCompany, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _notificationService.SendNotificationToAdminsAsync($"Car company deleted: {existingCompany.Name}", UserNotificationType.EntityDeleted);

        return Result<bool>.Success(true);
    }

    public async Task<Result<IEnumerable<CarCompanyNameDto>>> GetAllAsync(GetAllCarCompanyNameQuery request, CancellationToken cancellationToken = default)
    {
        var carColors = await _unitOfWork.Repository<CarCompanyName>()
            .AsQueryable()
            .ToListAsync(cancellationToken);

        var mapped = _mapper.Map<IEnumerable<CarCompanyNameDto>>(carColors);
        return Result.Success(mapped);
    }

    public async Task<Result<CarCompanyNameDto>> UpdateAsync(UpdateCarCompanyNameCommand request, CancellationToken cancellationToken = default)
    {
        var existingCompany = await _unitOfWork.Repository<CarCompanyName>()
           .FirstOrDefaultAsync(p => p.Id.ToString() == request.Id, cancellationToken);

        if (existingCompany is null)
            return Result<CarCompanyNameDto>.Error("This car company id does not exists");

        var company = await _unitOfWork.Repository<CarCompanyName>()
            .FirstOrDefaultAsync(p => p.Name == request.Name && p.Id.ToString() != request.Id, cancellationToken);

        if (company is not null)
            return Result<CarCompanyNameDto>.Error("This car company already exists");

        existingCompany.Name = request.Name!;

        await _unitOfWork.Repository<CarCompanyName>().UpdateAsync(existingCompany, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _notificationService.SendNotificationToAdminsAsync($"Car company updated: {existingCompany.Name}", UserNotificationType.EntityUpdated);

        return Result<CarCompanyNameDto>.Success(_mapper.Map<CarCompanyNameDto>(existingCompany));
    }
}
