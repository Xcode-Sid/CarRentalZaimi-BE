using AutoMapper;
using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Common.Messages;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.Privacy.Commands.CreatePrivacy;
using CarRentalZaimi.Application.Features.Privacy.Commands.DeletePrivacy;
using CarRentalZaimi.Application.Features.Privacy.Commands.UpdatePrivacy;
using CarRentalZaimi.Application.Features.Privacy.Queries.GetAllPagedPrivacies;
using CarRentalZaimi.Application.Features.Privacy.Queries.GetAllPrivacies;
using CarRentalZaimi.Application.Interfaces.Services;
using CarRentalZaimi.Application.Interfaces.UnitOfWork;
using CarRentalZaimi.Domain.Entities;
using CarRentalZaimi.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace CarRentalZaimi.Application.Services;

public class PrivacyService(IUnitOfWork _unitOfWork, IMapper _mapper, INotificationService _notificationService) : IPrivacyService
{
    public async Task<Result<PrivacyDto>> CreateAsync(CreatePrivacyCommand request, CancellationToken cancellationToken = default)
    {
        var newPrivacy = new Privacy
        {
            Title = request.Title,
            Description = request.Description,
            Color = request.Color,
            Icon = request.Icon,
            IsActive = request.IsActive
        };

        await _unitOfWork.Repository<Privacy>().AddAsync(newPrivacy, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _notificationService.SendNotificationToAdminsAsync($"New privacy policy added: {newPrivacy.Title}", UserNotificationType.EntityAdded);

        return Result<PrivacyDto>.Success(_mapper.Map<PrivacyDto>(newPrivacy));
    }

    public async Task<Result<bool>> DeleteAsync(DeletePrivacyCommand request, CancellationToken cancellationToken = default)
    {
        var existingPrivacy = await _unitOfWork.Repository<Privacy>()
            .FirstOrDefaultAsync(p => p.Id.ToString() == request.Id, cancellationToken);

        if (existingPrivacy is null)
            return Result<bool>.Error(ServiceErrorMessages.Privacy.NotFound);

        existingPrivacy.IsDeleted = true;

        await _unitOfWork.Repository<Privacy>().UpdateAsync(existingPrivacy, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _notificationService.SendNotificationToAdminsAsync($"Privacy policy deleted: {existingPrivacy.Title}", UserNotificationType.EntityDeleted);

        return Result<bool>.Success(true);
    }

    public async Task<Result<IEnumerable<PrivacyDto>>> GetAllAsync(GetAllPrivaciesQuery request, CancellationToken cancellationToken = default)
    {
        var privacies = await _unitOfWork.Repository<Privacy>()
          .AsQueryable()
          .ToListAsync(cancellationToken);

        var mapped = _mapper.Map<IEnumerable<PrivacyDto>>(privacies);
        return Result.Success(mapped);
    }

    public async Task<Result<PagedResponse<PrivacyDto>>> GetAllPagedAsync(GetAllPagedPrivaciesQuery request, CancellationToken cancellationToken = default)
    {
        var query = _unitOfWork.Repository<Privacy>()
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

        var mapped = _mapper.Map<List<PrivacyDto>>(cars);

        var pagedResponse = new PagedResponse<PrivacyDto>(mapped, totalCount, request.PageNr, request.PageSize);

        return Result<PagedResponse<PrivacyDto>>.Success(pagedResponse);
    }

    public async Task<Result<PrivacyDto>> UpdateAsync(UpdatePrivacyCommand request, CancellationToken cancellationToken = default)
    {
        var existingPrivacy = await _unitOfWork.Repository<Privacy>()
           .FirstOrDefaultAsync(p => p.Id.ToString() == request.Id, cancellationToken);

        if (existingPrivacy is null)
            return Result<PrivacyDto>.Error(ServiceErrorMessages.Privacy.NotFoundUpdate);

        existingPrivacy.Title = request.Title;
        existingPrivacy.Description = request.Description;
        existingPrivacy.Icon = request.Icon;
        existingPrivacy.Color = request.Color;
        existingPrivacy.IsActive = request.IsActive;

        await _unitOfWork.Repository<Privacy>().UpdateAsync(existingPrivacy, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _notificationService.SendNotificationToAdminsAsync($"Privacy policy updated: {existingPrivacy.Title}", UserNotificationType.EntityUpdated);

        return Result<PrivacyDto>.Success(_mapper.Map<PrivacyDto>(existingPrivacy));
    }
}
