using CarRentalZaimi.Domain.Common;

namespace CarRentalZaimi.Domain.Entities;

public class Ads : AuditedEntity<Guid>
{
    public string? Title { get; set; }
    public string? ImageName { get; set; }
    public string? ImageUrl { get; set; }
    public string? VideoName { get; set; }
    public string? VideoUrl { get; set; }
    public string? LinkUrl { get; set; }
    public string? Position { get; set; }
    public bool IsActive { get; set; }
}
