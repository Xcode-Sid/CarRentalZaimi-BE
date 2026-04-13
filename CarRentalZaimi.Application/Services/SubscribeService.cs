using AutoMapper;
using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.Subscribe.Commands.CreateSubscription;
using CarRentalZaimi.Application.Features.Subscribe.Commands.RemoveSubscription;
using CarRentalZaimi.Application.Features.Subscribe.Queries.GetAllPagedSubscriptions;
using CarRentalZaimi.Application.Interfaces.Services;
using CarRentalZaimi.Application.Interfaces.UnitOfWork;
using CarRentalZaimi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRentalZaimi.Application.Services;

public class SubscribeService(IUnitOfWork _unitOfWork, IMapper _mapper) : ISubscribeService
{
    public async Task<Result<PagedResponse<SubscribeDto>>> GetALlPagedSubscriptionsAsync(GetAllPagedSubscriptionsQuery request, CancellationToken cancellationToken = default)
    {
        var query = _unitOfWork.Repository<Subscribe>()
             .AsQueryable()
             .Where(c => !c.IsDeleted);

        // Search
        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.ToLower();
            query = query.Where(c =>
                (c.Email! != null && c.Email.ToLower().Contains(search)));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var subscribtions = await query
            .Skip((request.PageNr - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var mapped = _mapper.Map<List<SubscribeDto>>(subscribtions);

        var pagedResponse = new PagedResponse<SubscribeDto>(mapped, totalCount, request.PageNr, request.PageSize);

        return Result<PagedResponse<SubscribeDto>>.Success(pagedResponse);
    }

    public async Task<Result<SubscribeDto>> SubscribeAsync(CreateSubscriptionCommand request, CancellationToken cancellationToken = default)
    {
        var subscription = await _unitOfWork.Repository<Subscribe>()
            .FirstOrDefaultAsync(p => p.Email == request.Email, cancellationToken);

        if (subscription is not null)
            return Result<SubscribeDto>.Error("This email has already subscribed");

        var newSubscription = new Subscribe
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
        };

        await _unitOfWork.Repository<Subscribe>().AddAsync(newSubscription, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<SubscribeDto>.Success(_mapper.Map<SubscribeDto>(newSubscription));
    }

    public async Task<Result<bool>> UsubscribeAsync(RemoveSubscriptionCommand request, CancellationToken cancellationToken = default)
    {
        var subscription = await _unitOfWork.Repository<Subscribe>()
            .FirstOrDefaultAsync(p => p.Email == request.Email, cancellationToken);

        if (subscription is null)
            return Result<bool>.Error("This email does not hava any subscriptions");

        subscription.IsUnsubscribed = true;

        await _unitOfWork.Repository<Subscribe>().UpdateAsync(subscription, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(_mapper.Map<bool>(subscription));
    }
}
