using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.Notifications.Queries.GetUserNotificationById;

internal class GetUserNotificationByIdQueryHandler(INotificationQueryService _notificationQueryService)
    : IQueryHandler<GetUserNotificationByIdQuery, UserNotificationDto>
{
    public async Task<Result<UserNotificationDto>> Handle(
        GetUserNotificationByIdQuery request, CancellationToken cancellationToken)
        => await _notificationQueryService.GetUserNotificationByIdAsync(request.NotificationId, request.UserId, cancellationToken);
}
