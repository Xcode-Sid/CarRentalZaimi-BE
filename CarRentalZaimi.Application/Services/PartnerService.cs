using AutoMapper;
using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.Partner.Commands.CreatePartner;
using CarRentalZaimi.Application.Features.Partner.Commands.DeletePartner;
using CarRentalZaimi.Application.Features.Partner.Commands.UpdatePartner;
using CarRentalZaimi.Application.Features.Partner.Queries.GetAllPagedPartners;
using CarRentalZaimi.Application.Features.Partner.Queries.GetAllPartners;
using CarRentalZaimi.Application.Interfaces.Services;
using CarRentalZaimi.Application.Interfaces.UnitOfWork;
using CarRentalZaimi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRentalZaimi.Application.Services;

public class PartnerService(IUnitOfWork _unitOfWork, IMapper _mapper) : IPartnerService
{
    public async Task<Result<PartnerDto>> CreateAsync(CreatePartnerCommand request, CancellationToken cancellationToken = default)
    {
        var partner = await _unitOfWork.Repository<Partner>()
           .FirstOrDefaultAsync(p => p.Name == request.Name, cancellationToken);

        if (partner is not null)
            return Result<PartnerDto>.Error("This partner already exists");

        var newPartner = new Partner
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Color = request.Color,
            Initials = request.Initials,
            IsActive = request.IsActive,
        };

        await _unitOfWork.Repository<Partner>().AddAsync(newPartner, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<PartnerDto>.Success(_mapper.Map<PartnerDto>(newPartner));
    }

    public async Task<Result<bool>> DeleteAsync(DeletePartnerCommand request, CancellationToken cancellationToken = default)
    {
        var existingPartner = await _unitOfWork.Repository<Partner>()
            .FirstOrDefaultAsync(p => p.Id.ToString() == request.Id, cancellationToken);

        if (existingPartner is null)
            return Result<bool>.Error("This partner id does not exist");

        existingPartner.IsDeleted = true;

        await _unitOfWork.Repository<Partner>().UpdateAsync(existingPartner, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }

    public async Task<Result<IEnumerable<PartnerDto>>> GetAllAsync(GetAllPartnersQuery request, CancellationToken cancellationToken = default)
    {
        var carFuels = await _unitOfWork.Repository<Partner>()
          .AsQueryable()
          .ToListAsync(cancellationToken);

        var mapped = _mapper.Map<IEnumerable<PartnerDto>>(carFuels);
        return Result.Success(mapped);
    }

    public async Task<Result<PagedResponse<PartnerDto>>> GetAllPagedAsync(GetAllPagedPartnersQuery request, CancellationToken cancellationToken = default)
    {
        var query = _unitOfWork.Repository<Partner>()
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

        var cars = await query
            .Skip((request.PageNr - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var mapped = _mapper.Map<List<PartnerDto>>(cars);

        var pagedResponse = new PagedResponse<PartnerDto>(mapped, totalCount, request.PageNr, request.PageSize);

        return Result<PagedResponse<PartnerDto>>.Success(pagedResponse);
    }

    public async Task<Result<PartnerDto>> UpdateAsync(UpdatePartnerCommand request, CancellationToken cancellationToken = default)
    {
        var existingPartner = await _unitOfWork.Repository<Partner>()
           .FirstOrDefaultAsync(p => p.Id.ToString() == request.Id, cancellationToken);

        if (existingPartner is null)
            return Result<PartnerDto>.Error("This partner id does not exists");

        var partner = await _unitOfWork.Repository<Partner>()
            .FirstOrDefaultAsync(p => p.Name == request.Name && p.Id.ToString() != request.Id, cancellationToken);

        if (partner is not null)
            return Result<PartnerDto>.Error("This partnere already exists");

        existingPartner.Name = request.Name;
        existingPartner.Color = request.Color;
        existingPartner.Initials = request.Initials;
        existingPartner.IsActive = request.IsActive;

        await _unitOfWork.Repository<Partner>().UpdateAsync(existingPartner, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<PartnerDto>.Success(_mapper.Map<PartnerDto>(existingPartner));
    }
}
