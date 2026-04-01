using CarRentalZaimi.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace CarRentalZaimi.SignalR.Services;

public class NotificationService(
    IHubContext<NotificationHub> _hubContext,
    ILogger<NotificationService> _logger) : INotificationService
{
    public Task SendNotificationAsync(string message)
    {
        _logger.LogInformation("Sending notification: {Message}", message);
        throw new NotImplementedException();
    }
}
