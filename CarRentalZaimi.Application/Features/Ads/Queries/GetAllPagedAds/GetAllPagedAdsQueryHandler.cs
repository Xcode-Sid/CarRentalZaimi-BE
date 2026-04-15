using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.Ads.Queries.GetAllPagedAds;

internal class GetAllPagedAdsQueryHandler(IAdsService _adsService) : IQueryHandler<GetAllPagedAdsQuery, PagedResponse<AdsDto>>
{
    public async Task<Result<PagedResponse<AdsDto>>> Handle(GetAllPagedAdsQuery request, CancellationToken cancellationToken)
        => await _adsService.GetAllPagedAsync(request, cancellationToken);
}
