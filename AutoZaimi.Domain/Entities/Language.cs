using CarRentalZaimi.Domain.Common;

namespace CarRentalZaimi.Domain.Entities;

public class Language : AuditedEntity<Guid>
{
    public string? Name { get; set; }
    public string? NativeName { get; set; }
    public string? Code { get; set; }
    public string? IsoCode { get; set; }
    public string? CountryCode { get; set; }
    public bool IsActive { get; set; }
    public bool IsDefault { get; set; }
    public int SortOrder { get; set; }
    public string? FlagUrl { get; set; }
    public string? Description { get; set; }
}
