using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.Ads.Commands.UpdateAds;

public record UpdateAdsCommand : ICommand<AdsDto>
{
    public string? Id { get; set; }
    public string? Title { get; set; }
    public string? ImageData { get; set; }
    public string? ImageName { get; set; }
    public string? VideoName { get; set; }
    public string? VideoData { get; set; }
    public string? LinkUrl { get; set; }
    public string? Position { get; set; }
    public bool IsActive { get; set; }
}
