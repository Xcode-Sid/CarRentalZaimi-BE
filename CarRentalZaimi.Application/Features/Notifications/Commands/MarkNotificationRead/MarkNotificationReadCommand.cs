using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.Notifications.Commands.MarkNotificationRead;

public class MarkNotificationReadCommand : ICommand<bool>
{
    public Guid NotificationId { get; set; }
    public Guid UserId { get; set; }
}
