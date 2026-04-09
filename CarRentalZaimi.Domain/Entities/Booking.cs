using CarRentalZaimi.Domain.Common;
using CarRentalZaimi.Domain.Enums;

namespace CarRentalZaimi.Domain.Entities;

public class Booking : AuditedEntity<Guid>
{
    public User? User { get; set; }
    public Car? Car { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalPrice { get; set; }

    public string? PaymentMethod { get; set; }
    public BookingStatus? Status { get; set; }
    public ICollection<BookingService>? BookingServices { get; set; }

}
