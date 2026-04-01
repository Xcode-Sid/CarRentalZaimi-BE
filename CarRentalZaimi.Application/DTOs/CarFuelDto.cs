using CarRentalZaimi.Application.DTOs.Base;

namespace CarRentalZaimi.Application.DTOs;

public class CarFuelDto : BaseDto<Guid>
{
    public string? Name { get; set; }
}
