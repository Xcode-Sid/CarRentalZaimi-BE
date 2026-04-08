using CarRentalZaimi.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace CarRentalZaimi.Domain.Entities;

public class PasswordResetToken : AuditedEntity<Guid>
{
    [MaxLength(500)]
    public string? TokenHash { get; set; }
    [Required]
    public DateTime ExpiresAt { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }
    public bool IsUsed { get; set; }
    public DateTime? UsedAt { get; set; }
    public virtual User? User { get; set; }
}
