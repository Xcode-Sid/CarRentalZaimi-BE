using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.Notifications.Commands.DeleteNotification;

internal class DeleteNotificationCommandHandler(INotificationQueryService _notificationQueryService)
    : ICommandHandler<DeleteNotificationCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
        => await _notificationQueryService.DeleteNotificationAsync(request.NotificationId, request.UserId, cancellationToken);
}
