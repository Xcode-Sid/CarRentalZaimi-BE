using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.Notifications.Commands.DeleteNotification;

public class DeleteNotificationCommand : ICommand<bool>
{
    public Guid NotificationId { get; set; }
    public Guid UserId { get; set; }
}
