using CarRentalZaimi.Domain.Common;

namespace CarRentalZaimi.Domain.Entities
{
    public class Partner : AuditedEntity<Guid>
    {
        public string? Name { get; set; }
        public string? Initials { get; set; }
        public string? Color { get; set; }
        public bool IsActive { get; set; } 
    }
}
