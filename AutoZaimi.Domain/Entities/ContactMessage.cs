using CarRentalZaimi.Domain.Common;

namespace CarRentalZaimi.Domain.Entities;

public class ContactMessage : AuditedEntity<Guid>
{
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Subject { get; set; }
    public string? Message { get; set; }
    public bool IsRead { get; set; } = false;
    public DateTime? ReadAt { get; set; }
}
