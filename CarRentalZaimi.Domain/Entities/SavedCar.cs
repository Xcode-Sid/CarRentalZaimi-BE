using CarRentalZaimi.Domain.Common;

namespace CarRentalZaimi.Domain.Entities;

public class SavedCar : AuditedEntity<Guid>
{
    public virtual User? User { get; set; }
    public virtual Car? Car { get; set; }
}
