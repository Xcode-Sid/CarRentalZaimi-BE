using CarRentalZaimi.SignalR.Extentions;
using CarRentalZaimi.SignalR.Hubs;
using CarRentalZaimi.SignalR.Services;

namespace CarRentalZaimi.SignalR.Extentions;

public static class SignalRExtensions
{
    public static IServiceCollection AddSignalRServices(this IServiceCollection services)
    {
        services.AddSignalR();
        services.AddScoped<INotificationService, NotificationService>();
        return services;
    }

    public static WebApplication MapSignalRHubs(this WebApplication app)
    {
        app.MapHub<NotificationHub>("/hubs/notifications");
        return app;
    }
}
