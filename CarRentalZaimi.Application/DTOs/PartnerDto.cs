using CarRentalZaimi.Application.DTOs.Base;

namespace CarRentalZaimi.Application.DTOs;

public class PartnerDto : BaseDto<Guid>
{
    public string? Name { get; set; }
    public string? Initials { get; set; }
    public string? Color { get; set; }
    public bool IsActive { get; set; } = true;
}
