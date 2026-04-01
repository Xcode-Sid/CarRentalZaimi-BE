using CarRentalZaimi.Application.DTOs.Base;

namespace CarRentalZaimi.Application.DTOs;

public class CarImagesDto : BaseDto<Guid>
{
    public CarDto? Car { get; set; }
    public string? ImageName { get; set; }
    public string? ImagePath { get; set; }
    public bool IsPrimary { get; set; }
}
