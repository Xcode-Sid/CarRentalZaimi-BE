using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;

namespace CarRentalZaimi.Application.Features.Notifications.Queries.GetAdminNotificationById;

public class GetAdminNotificationByIdQuery : IQuery<UserNotificationDto>
{
    public Guid NotificationId { get; set; }
}
