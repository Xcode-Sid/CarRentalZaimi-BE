using CarRentalZaimi.Domain.Common;

namespace CarRentalZaimi.Domain.Entities;

public class Terms : AuditedEntity<Guid>
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Icon { get; set; }
    public string? Color { get; set; }
    public bool IsActive { get; set; }
}
