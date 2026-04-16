using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;

namespace CarRentalZaimi.Application.Features.Notifications.Queries.GetAdminNotifications;

public class GetAdminNotificationsQuery : IQuery<PagedResponse<UserNotificationDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
