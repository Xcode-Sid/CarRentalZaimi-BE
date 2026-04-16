using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.Notifications.Commands.MarkNotificationUnread;

internal class MarkNotificationUnreadCommandHandler(INotificationQueryService _notificationQueryService)
    : ICommandHandler<MarkNotificationUnreadCommand, bool>
{
    public async Task<Result<bool>> Handle(MarkNotificationUnreadCommand request, CancellationToken cancellationToken)
        => await _notificationQueryService.MarkAsUnreadAsync(request.NotificationId, request.UserId, cancellationToken);
}
