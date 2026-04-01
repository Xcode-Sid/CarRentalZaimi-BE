using CarRentalZaimi.Domain.Common;

namespace CarRentalZaimi.Domain.Entities;

public class CarReview : AuditedEntity<Guid>
{
    public required virtual User User { get; set; }
    public required virtual Car Post { get; set; }
    public required float Rating { get; set; }
    public string? Comment { get; set; }
}
