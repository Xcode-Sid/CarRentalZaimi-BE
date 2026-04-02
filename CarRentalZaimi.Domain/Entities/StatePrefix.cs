using CarRentalZaimi.Domain.Common;

namespace CarRentalZaimi.Domain.Entities;

public class StatePrefix : AuditedEntity<Guid>
{
    public string? CountryName { get; set; }
    public string? PhonePrefix { get; set; }
    public string? Flag { get; set; }
    public string? PhoneRegex { get; set; }
}

