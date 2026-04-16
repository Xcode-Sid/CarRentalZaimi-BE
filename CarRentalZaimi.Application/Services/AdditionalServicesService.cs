using AutoMapper;
using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Common.Messages;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.AdditionalService.Commands.CreateAdditionalService;
using CarRentalZaimi.Application.Features.AdditionalService.Commands.DeleteAdditionalService;
using CarRentalZaimi.Application.Features.AdditionalService.Commands.UpdateAdditionalService;
using CarRentalZaimi.Application.Features.AdditionalService.Queries.GetAllAdditionalServices;
using CarRentalZaimi.Application.Features.AdditionalService.Queries.GetAllPagedAdditionalServices;
using CarRentalZaimi.Application.Interfaces.Services;
using CarRentalZaimi.Application.Interfaces.UnitOfWork;
using CarRentalZaimi.Domain.Entities;
using CarRentalZaimi.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace CarRentalZaimi.Application.Services;

public class AdditionalServicesService(IUnitOfWork _unitOfWork, IMapper _mapper, INotificationService _notificationService) : IAdditionalServicesService
{
    public async Task<Result<AdditionalServiceDto>> CreateAsync(CreateAdditionalServiceCommand request, CancellationToken cancellationToken = default)
    {
        var service = new AdditionalService
        {
            Icon = request.Icon,
            Name = request.Name,
            PricePerDay = request.PricePerDay,
            IsActive = request.IsActive,
        };

        await _unitOfWork.Repository<AdditionalService>().AddAsync(service, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _notificationService.SendNotificationToAdminsAsync($"New additional service added: {service.Name}", UserNotificationType.EntityAdded);

        return Result<AdditionalServiceDto>.Success(_mapper.Map<AdditionalServiceDto>(service));
    }

    public async Task<Result<bool>> DeleteAsync(DeleteAdditionalServiceCommand request, CancellationToken cancellationToken = default)
    {
        var existingService = await _unitOfWork.Repository<AdditionalService>()
        .FirstOrDefaultAsync(p => p.Id.ToString() == request.Id, cancellationToken);

        if (existingService is null)
            return Result<bool>.Error(ServiceErrorMessages.AdditionalService.NotFound);

        existingService.IsDeleted = true;

        await _unitOfWork.Repository<AdditionalService>().UpdateAsync(existingService, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _notificationService.SendNotificationToAdminsAsync($"Additional service deleted: {existingService.Name}", UserNotificationType.EntityDeleted);

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

    public async Task<Result<PagedResponse<AdditionalServiceDto>>> GetAllPagedAsync(GetAllPagedAdditionalServicesQuery request, CancellationToken cancellationToken = default)
    {
        var query = _unitOfWork.Repository<AdditionalService>()
             .AsQueryable()
             .Where(c => !c.IsDeleted);

        // Search
        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.ToLower();
            query = query.Where(c =>
                (c.Name! != null && c.Name.ToLower().Contains(search)));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var services = await query
            .Skip((request.PageNr - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var mapped = _mapper.Map<List<AdditionalServiceDto>>(services);

        var pagedResponse = new PagedResponse<AdditionalServiceDto>(mapped, totalCount, request.PageNr, request.PageSize);

        return Result<PagedResponse<AdditionalServiceDto>>.Success(pagedResponse);
    }

    public async Task<Result<AdditionalServiceDto>> UpdateAsync(UpdateAdditionalServiceCommand request, CancellationToken cancellationToken = default)
    {
        var existingService = await _unitOfWork.Repository<AdditionalService>()
           .FirstOrDefaultAsync(p => p.Id.ToString() == request.Id, cancellationToken);

        if (existingService is null)
            return Result<AdditionalServiceDto>.Error(ServiceErrorMessages.AdditionalService.NotFoundUpdate);


        existingService.Name = request.Name;
        existingService.Icon = request.Icon;
        existingService.PricePerDay = request.PricePerDay;
        existingService.IsActive = request.IsActive;

        await _unitOfWork.Repository<AdditionalService>().UpdateAsync(existingService, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _notificationService.SendNotificationToAdminsAsync($"Additional service updated: {existingService.Name}", UserNotificationType.EntityUpdated);

        return Result<AdditionalServiceDto>.Success(_mapper.Map<AdditionalServiceDto>(existingService));
    }
}
