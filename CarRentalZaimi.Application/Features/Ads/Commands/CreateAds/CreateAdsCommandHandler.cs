using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.Ads.Commands.CreateAds;

internal class CreateAdsCommandHandler(IAdsService _adsService) : ICommandHandler<CreateAdsCommand, AdsDto>
{
    public async Task<Result<AdsDto>> Handle(CreateAdsCommand request, CancellationToken cancellationToken)
        => await _adsService.CreateAsync(request, cancellationToken);
}
