using CarRentalZaimi.Application.DTOs.Base;

namespace CarRentalZaimi.Application.DTOs;

public class StatePrefixDto : BaseDto<Guid>
{
    public string? CountryName { get; set; }
    public string? PhonePrefix { get; set; }
    public string? Flag { get; set; }
    public string? PhoneRegex { get; set; }
}
