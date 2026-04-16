using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.Notifications.Commands.MarkNotificationUnread;

public class MarkNotificationUnreadCommand : ICommand<bool>
{
    public Guid NotificationId { get; set; }
    public Guid UserId { get; set; }
}
