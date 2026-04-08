using AutoMapper;
using CarRentalZaimi.Application.Common.Errors;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.DTOs.ApiResponse;
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
    public async Task<ApiResponse<StatePrefixDto>> CreateAsync(CreateStatePrefixCommand request, CancellationToken cancellationToken = default)
    {
        var prefix = await _unitOfWork.Repository<StatePrefix>()
            .FirstOrDefaultAsync(p => p.PhonePrefix == request.PhonePrefix, cancellationToken);

        if (prefix is not null)
            return ApiResponse<StatePrefixDto>.FailureResponse(ErrorMessages.GetMessage(ErrorCodes.ALREADY_EXISTS));

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

        return ApiResponse<StatePrefixDto>.SuccessResponse(_mapper.Map<StatePrefixDto>(newPrefix));
    }

    public async Task<ApiResponse<StatePrefixDto>> UpdateAsync(UpdateStatePrefixCommand request, CancellationToken cancellationToken = default)
    {
        var existingPrefixId = await _unitOfWork.Repository<StatePrefix>()
            .FirstOrDefaultAsync(p => p.Id.ToString() == request.Id, cancellationToken);

        if (existingPrefixId is null)
            return ApiResponse<StatePrefixDto>.FailureResponse(ErrorMessages.GetMessage(ErrorCodes.NOT_FOUND));

        var prefix = await _unitOfWork.Repository<StatePrefix>()
            .FirstOrDefaultAsync(p => p.PhonePrefix == request.PhonePrefix && p.Id.ToString() != request.Id, cancellationToken);

        if (prefix is not null)
            return ApiResponse<StatePrefixDto>.FailureResponse(ErrorMessages.GetMessage(ErrorCodes.ALREADY_EXISTS));

        existingPrefixId.PhoneRegex = request.PhoneRegex;
        existingPrefixId.CountryName = request.CountryName;
        existingPrefixId.Flag = request.Flag;
        existingPrefixId.PhonePrefix = request.PhonePrefix;

        await _unitOfWork.Repository<StatePrefix>().UpdateAsync(existingPrefixId, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse<StatePrefixDto>.SuccessResponse(_mapper.Map<StatePrefixDto>(existingPrefixId));
    }

    public async Task<ApiResponse<bool>> DeleteAsync(DeleteStatePrefixCommand request, CancellationToken cancellationToken = default)
    {
        var existingPrefix = await _unitOfWork.Repository<StatePrefix>()
            .FirstOrDefaultAsync(p => p.Id.ToString() == request.Id, cancellationToken);

        if (existingPrefix is null)
            return ApiResponse<bool>.FailureResponse(ErrorMessages.GetMessage(ErrorCodes.NOT_FOUND));

        existingPrefix.IsDeleted = true;

        await _unitOfWork.Repository<StatePrefix>().UpdateAsync(existingPrefix, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse<bool>.SuccessResponse(true);
    }

    public async Task<ApiResponse<IEnumerable<StatePrefixDto>>> GetAllAsync(GetAllStatePrefixesQuery request, CancellationToken cancellationToken = default)
    {
        var statePrefixes = await _unitOfWork.Repository<StatePrefix>()
            .AsQueryable()
            .ToListAsync(cancellationToken);

        var mapped = _mapper.Map<IEnumerable<StatePrefixDto>>(statePrefixes);
        return ApiResponse<IEnumerable<StatePrefixDto>>.SuccessResponse(mapped);
    }
}
