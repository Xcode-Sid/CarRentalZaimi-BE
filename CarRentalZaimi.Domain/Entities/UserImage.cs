using CarRentalZaimi.Domain.Common;

namespace CarRentalZaimi.Domain.Entities;

public class UserImage : AuditedEntity<Guid>
{
    public string? UserId { get; set; }
    public virtual User? User { get; set; }
    public string? ImageName { get; set; }
    public string? ImagePath { get; set; }
}
