using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.Ads.Queries.GetAllAds;

internal class GetAllAdsQueryHandler(IAdsService _adsService) : IQueryHandler<GetAllAdsQuery, IEnumerable<AdsDto>>
{
    public async Task<Result<IEnumerable<AdsDto>>> Handle(GetAllAdsQuery request, CancellationToken cancellationToken)
        => await _adsService.GetAllAsync(request, cancellationToken);
}
