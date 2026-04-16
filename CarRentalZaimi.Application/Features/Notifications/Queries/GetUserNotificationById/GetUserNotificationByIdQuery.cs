using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;

namespace CarRentalZaimi.Application.Features.Notifications.Queries.GetUserNotificationById;

public class GetUserNotificationByIdQuery : IQuery<UserNotificationDto>
{
    public Guid NotificationId { get; set; }
    public Guid UserId { get; set; }
}
