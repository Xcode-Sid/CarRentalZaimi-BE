using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.Notifications.Commands.AdminMarkNotificationRead;

internal class AdminMarkNotificationReadCommandHandler(INotificationQueryService _notificationQueryService)
    : ICommandHandler<AdminMarkNotificationReadCommand, bool>
{
    public async Task<Result<bool>> Handle(AdminMarkNotificationReadCommand request, CancellationToken cancellationToken)
        => await _notificationQueryService.AdminMarkAsReadAsync(request.NotificationId, cancellationToken);
}
