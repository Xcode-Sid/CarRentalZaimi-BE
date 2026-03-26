
using CarRentalZaimi.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace CarRentalZaimi.SignalR.Services
{
    public class NotificationService(IHubContext<NotificationHub> _hubContext) : INotificationService
    {
        public Task SendNotificationAsync(string message)
        {
            throw new NotImplementedException();
        }
    }
}
