using CarRentalZaimi.Domain.Common;
using CarRentalZaimi.Domain.Enums;

namespace CarRentalZaimi.Domain.Entities;

public class UserNotification : AuditedEntity<Guid>
{
    public required virtual User User { get; set; }
    public string? Message { get; set; }
    public bool IsRead { get; set; }
    public required UserNotificationType UserNotificationType { get; set; }
}
