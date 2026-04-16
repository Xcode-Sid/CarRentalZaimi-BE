using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;

namespace CarRentalZaimi.Application.Interfaces.Services;

public interface INotificationQueryService
{
    // User
    Task<Result<PagedResponse<UserNotificationDto>>> GetUserNotificationsAsync(Guid userId, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    Task<Result<UserNotificationDto>> GetUserNotificationByIdAsync(Guid notificationId, Guid userId, CancellationToken cancellationToken = default);
    Task<Result<int>> GetUnreadCountAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<Result<bool>> MarkAsReadAsync(Guid notificationId, Guid userId, CancellationToken cancellationToken = default);
    Task<Result<bool>> MarkAsUnreadAsync(Guid notificationId, Guid userId, CancellationToken cancellationToken = default);
    Task<Result<bool>> MarkAllAsReadAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<Result<bool>> DeleteNotificationAsync(Guid notificationId, Guid userId, CancellationToken cancellationToken = default);

    // Admin
    Task<Result<PagedResponse<UserNotificationDto>>> GetAllNotificationsAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    Task<Result<UserNotificationDto>> GetNotificationByIdAdminAsync(Guid notificationId, CancellationToken cancellationToken = default);
    Task<Result<bool>> AdminMarkAsReadAsync(Guid notificationId, CancellationToken cancellationToken = default);
    Task<Result<bool>> AdminDeleteNotificationAsync(Guid notificationId, CancellationToken cancellationToken = default);
}
