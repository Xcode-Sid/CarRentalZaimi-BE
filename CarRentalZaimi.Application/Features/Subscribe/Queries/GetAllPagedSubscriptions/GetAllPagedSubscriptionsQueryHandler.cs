using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.CarReview.Queries.GetAllPagedCarReview;
using CarRentalZaimi.Application.Interfaces.Query;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.Subscribe.Queries.GetAllPagedSubscriptions;

internal class GetAllPagedSubscriptionsQueryHandler(ISubscribeService _subscribeService) : IQueryHandler<GetAllPagedSubscriptionsQuery, PagedResponse<SubscribeDto>>
{
    public async Task<Result<PagedResponse<SubscribeDto>>> Handle(GetAllPagedSubscriptionsQuery request, CancellationToken cancellationToken)
        => await _subscribeService.GetALlPagedSubscriptionsAsync(request, cancellationToken);
}