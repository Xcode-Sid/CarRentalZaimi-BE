using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;

namespace CarRentalZaimi.Application.Features.Ads.Queries.GetAllPagedAds;

public class GetAllPagedAdsQuery : IQuery<PagedResponse<AdsDto>>
{
    public string? Search { get; set; }
    public int PageNr { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
