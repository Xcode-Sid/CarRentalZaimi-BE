using AutoMapper;
using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.Terms.Commands.CreateTerm;
using CarRentalZaimi.Application.Features.Terms.Commands.DeleteTerm;
using CarRentalZaimi.Application.Features.Terms.Commands.UpdateTerm;
using CarRentalZaimi.Application.Features.Terms.Queries.GetAllPagedTerms;
using CarRentalZaimi.Application.Features.Terms.Queries.GetAllTerms;
using CarRentalZaimi.Application.Interfaces.Services;
using CarRentalZaimi.Application.Interfaces.UnitOfWork;
using CarRentalZaimi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRentalZaimi.Application.Services;

public class TermsService(IUnitOfWork _unitOfWork, IMapper _mapper) : ITermsService
{
    public async Task<Result<TermsDto>> CreateAsync(CreateTermCommand request, CancellationToken cancellationToken = default)
    {
        var newTerm = new Terms
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            Color = request.Color,
            Icon = request.Icon,
            IsActive = request.IsActive,
        };

        await _unitOfWork.Repository<Terms>().AddAsync(newTerm, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<TermsDto>.Success(_mapper.Map<TermsDto>(newTerm));
    }

    public async Task<Result<bool>> DeleteAsync(DeleteTermCommand request, CancellationToken cancellationToken = default)
    {
        var existingTerm = await _unitOfWork.Repository<Terms>()
            .FirstOrDefaultAsync(p => p.Id.ToString() == request.Id, cancellationToken);

        if (existingTerm is null)
            return Result<bool>.Error("This term id does not exist");

        existingTerm.IsDeleted = true;

        await _unitOfWork.Repository<Terms>().UpdateAsync(existingTerm, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }

    public async Task<Result<IEnumerable<TermsDto>>> GetAllAsync(GetAllTermsQuery request, CancellationToken cancellationToken = default)
    {
        var carFuels = await _unitOfWork.Repository<Terms>()
          .AsQueryable()
          .ToListAsync(cancellationToken);

        var mapped = _mapper.Map<IEnumerable<TermsDto>>(carFuels);
        return Result.Success(mapped);
    }

    public async Task<Result<PagedResponse<TermsDto>>> GetAllPagedAsync(GetAllPagedTermsQuery request, CancellationToken cancellationToken = default)
    {
        var query = _unitOfWork.Repository<Terms>()
             .AsQueryable()
             .Where(c => !c.IsDeleted);

        // Search
        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.ToLower();
            query = query.Where(c =>
                (c.Title! != null && c.Title.ToLower().Contains(search)) ||
                (c.Description! != null && c.Description.ToLower().Contains(search)));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var cars = await query
            .Skip((request.PageNr - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var mapped = _mapper.Map<List<TermsDto>>(cars);

        var pagedResponse = new PagedResponse<TermsDto>(mapped, totalCount, request.PageNr, request.PageSize);

        return Result<PagedResponse<TermsDto>>.Success(pagedResponse);
    }

    public async Task<Result<TermsDto>> UpdateAsync(UpdateTermCommand request, CancellationToken cancellationToken = default)
    {
        var existingTerm = await _unitOfWork.Repository<Terms>()
           .FirstOrDefaultAsync(p => p.Id.ToString() == request.Id, cancellationToken);

        if (existingTerm is null)
            return Result<TermsDto>.Error("This term id does not exists");

        existingTerm.Title = request.Title;
        existingTerm.Description = request.Description;
        existingTerm.Icon = request.Icon;
        existingTerm.Color = request.Color;
        existingTerm.IsActive = request.IsActive;

        await _unitOfWork.Repository<Terms>().UpdateAsync(existingTerm, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<TermsDto>.Success(_mapper.Map<TermsDto>(existingTerm));
    }
}
