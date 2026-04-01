using CarRentalZaimi.Application.DTOs.Base;

namespace CarRentalZaimi.Application.DTOs;

public class CarInteriorColorDto : BaseDto<Guid>
{
    public string? Name { get; set; }
}
