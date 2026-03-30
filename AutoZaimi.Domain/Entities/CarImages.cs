using CarRentalZaimi.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace CarRentalZaimi.Domain.Entities;

public class CarImages : AuditedEntity<Guid>
{
    public virtual Car? Car { get; set; }

    [StringLength(255)]
    public string? ImageName { get; set; }

    [StringLength(255)]
    public string? ImagePath { get; set; }
    public bool IsPrimary { get; set; }
}
