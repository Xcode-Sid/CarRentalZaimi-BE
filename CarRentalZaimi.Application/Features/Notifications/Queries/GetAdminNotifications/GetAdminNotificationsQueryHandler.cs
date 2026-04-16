using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.Notifications.Queries.GetAdminNotifications;

internal class GetAdminNotificationsQueryHandler(INotificationQueryService _notificationQueryService)
    : IQueryHandler<GetAdminNotificationsQuery, PagedResponse<UserNotificationDto>>
{
    public async Task<Result<PagedResponse<UserNotificationDto>>> Handle(
        GetAdminNotificationsQuery request, CancellationToken cancellationToken)
        => await _notificationQueryService.GetAllNotificationsAsync(request.PageNumber, request.PageSize, cancellationToken);
}
