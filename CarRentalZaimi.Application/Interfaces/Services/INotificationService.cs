using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Domain.Enums;

namespace CarRentalZaimi.Application.Interfaces.Services;

public interface INotificationService
{
    Task SendNotificationToUserAsync(Guid userId, string message, UserNotificationType type);
    Task<Result<bool>> SendNotificationToAdminsAsync(string message, UserNotificationType type);
    Task SendNotificationToAllAsync(string message, UserNotificationType type);
}
