using CarRentalZaimi.Application.DTOs.Base;

namespace CarRentalZaimi.Application.DTOs;

public class AdsDto : BaseDto<Guid>
{
    public string? Title { get; set; }
    public string? ImageName { get; set; }
    public string? ImageUrl { get; set; }
    public string? VideoName { get; set; }
    public string? VideoUrl { get; set; }
    public string? LinkUrl { get; set; }
    public string? Position { get; set; }
    public bool IsActive { get; set; }
}
