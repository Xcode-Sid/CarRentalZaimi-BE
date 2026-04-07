using CarRentalZaimi.Application.DTOs.Base;

namespace CarRentalZaimi.Application.DTOs;

public class CarImagesDto : BaseDto<Guid>
{
    public string? ImageName { get; set; }
    public string? ImagePath { get; set; }
    public bool IsPrimary { get; set; }
}
