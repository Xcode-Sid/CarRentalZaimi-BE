using CarRentalZaimi.Domain.Common;
using CarRentalZaimi.Domain.Enums;

namespace CarRentalZaimi.Domain.Entities
{
    public class OccupiedCarDays : AuditedEntity<Guid>
    {
        public required Car Car { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public CarBlockedDateType Type { get; set; }
    }
}
