using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.Notifications.Commands.AdminDeleteNotification;

public class AdminDeleteNotificationCommand : ICommand<bool>
{
    public Guid NotificationId { get; set; }
}
