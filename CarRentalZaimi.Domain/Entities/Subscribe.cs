using CarRentalZaimi.Domain.Common;

namespace CarRentalZaimi.Domain.Entities;

public class Subscribe : AuditedEntity<Guid>
{
    public string? Email { get; set; }
    public bool IsUnsubscribed { get; set; }
}
