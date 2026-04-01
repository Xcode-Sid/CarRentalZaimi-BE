using CarRentalZaimi.Application.DTOs.Base;

namespace CarRentalZaimi.Application.DTOs;

public class CompanyProfileDto : BaseDto<Guid>
{
    public string? Name { get; set; }
    public string? LogoUrl { get; set; }
    public string? Tagline { get; set; }
    public string? AboutText { get; set; }
    public string? MissionTitle { get; set; }
    public string? MissionDescription { get; set; }
    public string? WhyChooseUs { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? WorkingHours { get; set; }
    public string? FacebookUrl { get; set; }
    public string? InstagramUrl { get; set; }
    public string? TwiterUrl { get; set; }
    public string? YoutubeUrl { get; set; }
    public string? WhatsAppNumber { get; set; }
}
