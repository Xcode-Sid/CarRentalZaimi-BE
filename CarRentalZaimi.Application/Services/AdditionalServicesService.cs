using AutoMapper;
using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.AdditionalService.Commands.CreateAdditionalService;
using CarRentalZaimi.Application.Features.AdditionalService.Commands.DeleteAdditionalService;
using CarRentalZaimi.Application.Features.AdditionalService.Commands.UpdateAdditionalService;
using CarRentalZaimi.Application.Features.AdditionalService.Queries.GetAllAdditionalServices;
using CarRentalZaimi.Application.Interfaces.Services;
using CarRentalZaimi.Application.Interfaces.UnitOfWork;
using CarRentalZaimi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRentalZaimi.Application.Services;

public class AdditionalServicesService(IUnitOfWork _unitOfWork, IMapper _mapper) : IAdditionalServicesService
{
    public async Task<Result<AdditionalServiceDto>> CreateAsync(CreateAdditionalServiceCommand request, CancellationToken cancellationToken = default)
    {
        var service = new AdditionalService
        {
            Id = Guid.NewGuid(),
            Icon = request.Icon,
            Name = request.Name,
            PricePerDay = request.PricePerDay,
            IsActive = request.IsActive,
        };

        await _unitOfWork.Repository<AdditionalService>().AddAsync(service, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<AdditionalServiceDto>.Success(_mapper.Map<AdditionalServiceDto>(service));
    }

    public async Task<Result<bool>> DeleteAsync(DeleteAdditionalServiceCommand request, CancellationToken cancellationToken = default)
    {
        var existingService = await _unitOfWork.Repository<AdditionalService>()
        .FirstOrDefaultAsync(p => p.Id.ToString() == request.Id, cancellationToken);

        if (existingService is null)
            return Result<bool>.Error("This service id does not exist");

        existingService.IsDeleted = true;

        await _unitOfWork.Repository<AdditionalService>().UpdateAsync(existingService, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }

    public async Task<Result<IEnumerable<AdditionalServiceDto>>> GetAllAsync(GetAllAdditionalServicesQuery request, CancellationToken cancellationToken = default)
    {
        var services = await _unitOfWork.Repository<AdditionalService>()
         .AsQueryable()
         .ToListAsync(cancellationToken);

        var mapped = _mapper.Map<IEnumerable<AdditionalServiceDto>>(services);
        return Result.Success(mapped);
    }

    public async Task<Result<AdditionalServiceDto>> UpdateAsync(UpdateAdditionalServiceCommand request, CancellationToken cancellationToken = default)
    {
        var existingService = await _unitOfWork.Repository<AdditionalService>()
           .FirstOrDefaultAsync(p => p.Id.ToString() == request.Id, cancellationToken);

        if (existingService is null)
            return Result<AdditionalServiceDto>.Error("This service id does not exists");


        existingService.Name = request.Name;
        existingService.Icon = request.Icon;
        existingService.PricePerDay = request.PricePerDay;
        existingService.IsActive = request.IsActive;

        await _unitOfWork.Repository<AdditionalService>().UpdateAsync(existingService, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<AdditionalServiceDto>.Success(_mapper.Map<AdditionalServiceDto>(existingService));
    }
}
