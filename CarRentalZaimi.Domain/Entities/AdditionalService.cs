using CarRentalZaimi.Domain.Common;

namespace CarRentalZaimi.Domain.Entities;

public class AdditionalService : AuditedEntity<Guid>
{
    public string? Name { get; set; }
    public string? Icon { get; set; }
    public decimal PricePerDay { get; set; }
    public bool IsActive { get; set; } = true;
    public ICollection<BookingService>? BookingServices { get; set; }
}
