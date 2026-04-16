using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.Notifications.Commands.AdminDeleteNotification;

internal class AdminDeleteNotificationCommandHandler(INotificationQueryService _notificationQueryService)
    : ICommandHandler<AdminDeleteNotificationCommand, bool>
{
    public async Task<Result<bool>> Handle(AdminDeleteNotificationCommand request, CancellationToken cancellationToken)
        => await _notificationQueryService.AdminDeleteNotificationAsync(request.NotificationId, cancellationToken);
}
