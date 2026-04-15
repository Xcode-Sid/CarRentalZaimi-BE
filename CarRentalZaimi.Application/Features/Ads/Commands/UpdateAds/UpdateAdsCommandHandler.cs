using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.Ads.Commands.UpdateAds;

internal class UpdateAdsCommandHandler(IAdsService _adsService) : ICommandHandler<UpdateAdsCommand, AdsDto>
{
    public async Task<Result<AdsDto>> Handle(UpdateAdsCommand request, CancellationToken cancellationToken)
        => await _adsService.UpdateAsync(request, cancellationToken);
}

