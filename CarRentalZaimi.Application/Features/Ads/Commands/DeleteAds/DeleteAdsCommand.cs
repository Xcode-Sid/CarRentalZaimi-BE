using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.Ads.Commands.DeleteAds;

public class DeleteAdsCommand : ICommand<bool>
{
    public string? Id { get; set; }
}
