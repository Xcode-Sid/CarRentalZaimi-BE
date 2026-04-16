using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.Notifications.Commands.AdminMarkNotificationRead;

public class AdminMarkNotificationReadCommand : ICommand<bool>
{
    public Guid NotificationId { get; set; }
}
