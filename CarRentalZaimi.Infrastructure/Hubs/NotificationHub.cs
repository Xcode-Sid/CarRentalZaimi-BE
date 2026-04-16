using CarRentalZaimi.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace CarRentalZaimi.Infrastructure.Hubs;

[Authorize]
public class NotificationHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
    }

    public async Task SendNotification(string message, UserNotificationType notificationType)
    {
        await Clients.All.SendAsync("ReceiveNotification", new
        {
            Message = message,
            NotificationType = notificationType,
            CreatedOn = DateTime.UtcNow
        });
    }
}
