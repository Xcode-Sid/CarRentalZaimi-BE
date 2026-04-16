using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.Notifications.Queries.GetAdminNotificationById;

internal class GetAdminNotificationByIdQueryHandler(INotificationQueryService _notificationQueryService)
    : IQueryHandler<GetAdminNotificationByIdQuery, UserNotificationDto>
{
    public async Task<Result<UserNotificationDto>> Handle(
        GetAdminNotificationByIdQuery request, CancellationToken cancellationToken)
        => await _notificationQueryService.GetNotificationByIdAdminAsync(request.NotificationId, cancellationToken);
}
