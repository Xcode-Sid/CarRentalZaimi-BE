using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.Notifications.Queries.GetUserNotifications;

internal class GetUserNotificationsQueryHandler(INotificationQueryService _notificationQueryService)
    : IQueryHandler<GetUserNotificationsQuery, PagedResponse<UserNotificationDto>>
{
    public async Task<Result<PagedResponse<UserNotificationDto>>> Handle(
        GetUserNotificationsQuery request, CancellationToken cancellationToken)
        => await _notificationQueryService.GetUserNotificationsAsync(request.UserId, request.PageNumber, request.PageSize, cancellationToken);
}
