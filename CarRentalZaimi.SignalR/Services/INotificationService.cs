namespace CarRentalZaimi.SignalR.Services;

public interface INotificationService
{
    Task SendNotificationAsync(string message);
}
