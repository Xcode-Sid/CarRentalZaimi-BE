using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.Notifications.Commands.MarkNotificationRead;

internal class MarkNotificationReadCommandHandler(INotificationQueryService _notificationQueryService)
    : ICommandHandler<MarkNotificationReadCommand, bool>
{
    public async Task<Result<bool>> Handle(MarkNotificationReadCommand request, CancellationToken cancellationToken)
        => await _notificationQueryService.MarkAsReadAsync(request.NotificationId, request.UserId, cancellationToken);
}
