using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.Notifications.Commands.MarkAllNotificationsRead;

internal class MarkAllNotificationsReadCommandHandler(INotificationQueryService _notificationQueryService)
    : ICommandHandler<MarkAllNotificationsReadCommand, bool>
{
    public async Task<Result<bool>> Handle(MarkAllNotificationsReadCommand request, CancellationToken cancellationToken)
        => await _notificationQueryService.MarkAllAsReadAsync(request.UserId, cancellationToken);
}
