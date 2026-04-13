using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.Subscribe.Commands.CreateSubscription;
using CarRentalZaimi.Application.Features.Subscribe.Commands.RemoveSubscription;
using CarRentalZaimi.Application.Features.Subscribe.Queries.GetAllPagedSubscriptions;

namespace CarRentalZaimi.Application.Interfaces.Services;

public interface ISubscribeService
{
    Task<Result<SubscribeDto>> SubscribeAsync(CreateSubscriptionCommand request, CancellationToken cancellationToken = default); 
    Task<Result<bool>> UsubscribeAsync(RemoveSubscriptionCommand request, CancellationToken cancellationToken = default);
    Task<Result<PagedResponse<SubscribeDto>>> GetALlPagedSubscriptionsAsync(GetAllPagedSubscriptionsQuery request, CancellationToken cancellationToken = default);
}
