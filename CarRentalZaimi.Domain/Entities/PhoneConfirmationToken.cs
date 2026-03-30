using CarRentalZaimi.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace CarRentalZaimi.Domain.Entities;

public class PhoneConfirmationToken : AuditedEntity<Guid>
{
    [MaxLength(500)]
    public string? Code { get; set; }
    [Required]
    public DateTime ExpiresAt { get; set; }
    public bool IsUsed { get; set; }
    public DateTime? UsedAt { get; set; }
    public virtual User? User { get; set; }
}