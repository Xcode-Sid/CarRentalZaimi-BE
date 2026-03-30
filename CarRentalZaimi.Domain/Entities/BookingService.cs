using CarRentalZaimi.Domain.Common;

namespace CarRentalZaimi.Domain.Entities;

public class BookingService : AuditedEntity<Guid>
{
    public Guid BookingId { get; set; }
    public virtual Booking? Booking { get; set; }
    public virtual AdditionalService? AdditionalService { get; set; }
    public decimal PricePerDay { get; set; }
}
