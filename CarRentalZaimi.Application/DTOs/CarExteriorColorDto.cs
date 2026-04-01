using CarRentalZaimi.Application.DTOs.Base;

namespace CarRentalZaimi.Application.DTOs;

public class CarExteriorColorDto : BaseDto<Guid>
{
    public string? Name { get; set; }
}
