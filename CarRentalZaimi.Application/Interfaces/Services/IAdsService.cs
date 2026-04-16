using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.Ads.Commands.CreateAds;
using CarRentalZaimi.Application.Features.Ads.Commands.DeleteAds;
using CarRentalZaimi.Application.Features.Ads.Commands.UpdateAds;
using CarRentalZaimi.Application.Features.Ads.Queries.GetAllAds;
using CarRentalZaimi.Application.Features.Ads.Queries.GetAllPagedAds;

namespace CarRentalZaimi.Application.Interfaces.Services;

public interface IAdsService
{
    Task<Result<AdsDto>> CreateAsync(CreateAdsCommand request, CancellationToken cancellationToken = default);
    Task<Result<AdsDto>> UpdateAsync(UpdateAdsCommand request, CancellationToken cancellationToken = default);
    Task<Result<bool>> DeleteAsync(DeleteAdsCommand request, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<AdsDto>>> GetAllAsync(GetAllAdsQuery request, CancellationToken cancellationToken = default);
    Task<Result<PagedResponse<AdsDto>>> GetAllPagedAsync(GetAllPagedAdsQuery request, CancellationToken cancellationToken = default);
}
