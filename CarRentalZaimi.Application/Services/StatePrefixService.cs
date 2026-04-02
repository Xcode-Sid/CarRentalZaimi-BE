using AutoMapper;
using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.StatePrefixes.Commands.CreateStatePrefix;
using CarRentalZaimi.Application.Features.StatePrefixes.Commands.DeleteStatePrefix;
using CarRentalZaimi.Application.Features.StatePrefixes.Commands.UpdateStatePrefix;
using CarRentalZaimi.Application.Features.StatePrefixes.Queries;
using CarRentalZaimi.Application.Interfaces.Services;
using CarRentalZaimi.Application.Interfaces.UnitOfWork;
using CarRentalZaimi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRentalZaimi.Application.Services;

public class StatePrefixService(IUnitOfWork _unitOfWork, IMapper _mapper) : IStatePrefixService
{
    public async Task<Result<StatePrefixDto>> CreateAsync(CreateStatePrefixCommand request, CancellationToken cancellationToken = default)
    {
        var prefix = await _unitOfWork.Repository<StatePrefix>()
            .FirstOrDefaultAsync(p => p.PhonePrefix == request.PhonePrefix, cancellationToken);

        if (prefix is not null)
            return Result<StatePrefixDto>.Error("This prefix already exists");

        var newPrefix = new StatePrefix
        {
            Id = Guid.NewGuid(),
            PhonePrefix = request.PhonePrefix,
            CountryName = request.CountryName,
            Flag = request.Flag,
            PhoneRegex = request.PhoneRegex,
        };

        await _unitOfWork.Repository<StatePrefix>().AddAsync(newPrefix, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<StatePrefixDto>.Success(_mapper.Map<StatePrefixDto>(newPrefix));
    }

    public async Task<Result<StatePrefixDto>> UpdateAsync(UpdateStatePrefixCommand request, CancellationToken cancellationToken = default)
    {
        var existingPrefixId = await _unitOfWork.Repository<StatePrefix>()
            .FirstOrDefaultAsync(p => p.Id.ToString() == request.Id, cancellationToken);

        if (existingPrefixId is null)
            return Result<StatePrefixDto>.Error("This prefix id does not exists");

        var prefix = await _unitOfWork.Repository<StatePrefix>()
            .FirstOrDefaultAsync(p => p.PhonePrefix == request.PhonePrefix && p.Id.ToString() != request.Id, cancellationToken);

        if (prefix is not null)
            return Result<StatePrefixDto>.Error("This prefix already exists");

        existingPrefixId.PhoneRegex = request.PhoneRegex;
        existingPrefixId.CountryName = request.CountryName;
        existingPrefixId.Flag = request.Flag;
        existingPrefixId.PhonePrefix = request.PhonePrefix;

        await _unitOfWork.Repository<StatePrefix>().UpdateAsync(existingPrefixId, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<StatePrefixDto>.Success(_mapper.Map<StatePrefixDto>(existingPrefixId));
    }

    public async Task<Result<bool>> DeleteAsync(DeleteStatePrefixCommand request, CancellationToken cancellationToken = default)
    {
        var existingPrefix = await _unitOfWork.Repository<StatePrefix>()
            .FirstOrDefaultAsync(p => p.Id.ToString() == request.Id, cancellationToken);

        if (existingPrefix is null)
            return Result<bool>.Error("This prefix id does not exist");

        existingPrefix.IsDeleted = true;

        await _unitOfWork.Repository<StatePrefix>().UpdateAsync(existingPrefix, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }

    public async Task<Result<IEnumerable<StatePrefixDto>>> GetAllAsync(GetAllStatePrefixesQuery request, CancellationToken cancellationToken = default)
    {
        var statePrefixes = await _unitOfWork.Repository<StatePrefix>()
            .AsQueryable()
            .ToListAsync(cancellationToken);

        var mapped = _mapper.Map<IEnumerable<StatePrefixDto>>(statePrefixes);
        return Result.Success(mapped);
    }
}
