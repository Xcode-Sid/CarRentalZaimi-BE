using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.Ads.Commands.DeleteAds;

internal class DeleteAdsCommandHandler(IAdsService _adsService) : ICommandHandler<DeleteAdsCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteAdsCommand request, CancellationToken cancellationToken)
        => await _adsService.DeleteAsync(request, cancellationToken);
}

