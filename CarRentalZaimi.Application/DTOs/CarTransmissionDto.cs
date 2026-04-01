using CarRentalZaimi.Application.DTOs.Base;

namespace CarRentalZaimi.Application.DTOs;

public class CarTransmissionDto : BaseDto<Guid>
{
    public string? Name { get; set; }
}
