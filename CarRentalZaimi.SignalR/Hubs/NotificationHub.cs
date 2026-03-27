using Microsoft.AspNetCore.SignalR;

namespace CarRentalZaimi.SignalR.Hubs;

public class NotificationHub : Hub  //TODO check later
{
    public async Task SendNotification(string message)
    {
        await Clients.All.SendAsync("ReceiveNotification", message);
    }

    public async Task SendNotificationToUser(string userId, string message)
    {
        await Clients.User(userId).SendAsync("ReceiveNotification", message);
    }
}
