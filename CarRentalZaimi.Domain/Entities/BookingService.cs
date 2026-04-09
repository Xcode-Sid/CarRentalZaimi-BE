using CarRentalZaimi.Domain.Common;

namespace CarRentalZaimi.Domain.Entities;

public class BookingService : AuditedEntity<Guid>
{
    public virtual Booking? Booking { get; set; }
    public virtual AdditionalService? AdditionalService { get; set; }
}
