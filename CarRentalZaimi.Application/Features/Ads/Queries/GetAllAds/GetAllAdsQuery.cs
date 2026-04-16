using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;

namespace CarRentalZaimi.Application.Features.Ads.Queries.GetAllAds;

public class GetAllAdsQuery : IQuery<IEnumerable<AdsDto>>;
