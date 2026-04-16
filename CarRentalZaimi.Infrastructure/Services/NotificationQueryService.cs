using AutoMapper;
using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Common.Messages;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Services;
using CarRentalZaimi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CarRentalZaimi.Infrastructure.Services;

public class NotificationQueryService(ApplicationDbContext dbContext, IMapper mapper) : INotificationQueryService
{
    // ─── User ────────────────────────────────────────────────────────────────

    public async Task<Result<PagedResponse<UserNotificationDto>>> GetUserNotificationsAsync(
        Guid userId, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        var query = dbContext.UserNotifications
            .Include(n => n.User)
            .Where(n => n.User.Id == userId.ToString() && !n.IsDeleted)
            .OrderByDescending(n => n.CreatedOn);

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var mapped = mapper.Map<List<UserNotificationDto>>(items);
        return Result<PagedResponse<UserNotificationDto>>.Success(
            new PagedResponse<UserNotificationDto>(mapped, totalCount, pageNumber, pageSize));
    }

    public async Task<Result<UserNotificationDto>> GetUserNotificationByIdAsync(
        Guid notificationId, Guid userId, CancellationToken cancellationToken = default)
    {
        var notification = await dbContext.UserNotifications
            .Include(n => n.User)
            .FirstOrDefaultAsync(n => n.Id == notificationId && n.User.Id == userId.ToString() && !n.IsDeleted, cancellationToken);

        if (notification is null)
            return Result<UserNotificationDto>.Error(ServiceErrorMessages.Notification.NotFoundOrForbidden);

        return Result<UserNotificationDto>.Success(mapper.Map<UserNotificationDto>(notification));
    }

    public async Task<Result<int>> GetUnreadCountAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var count = await dbContext.UserNotifications
            .Where(n => n.User.Id == userId.ToString() && !n.IsRead && !n.IsDeleted)
            .CountAsync(cancellationToken);

        return Result<int>.Success(count);
    }

    public async Task<Result<bool>> MarkAsReadAsync(
        Guid notificationId, Guid userId, CancellationToken cancellationToken = default)
    {
        var notification = await dbContext.UserNotifications
            .Include(n => n.User)
            .FirstOrDefaultAsync(n => n.Id == notificationId && n.User.Id == userId.ToString() && !n.IsDeleted, cancellationToken);

        if (notification is null)
            return Result<bool>.Error(ServiceErrorMessages.Notification.NotFoundOrForbidden);

        notification.IsRead = true;
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> MarkAsUnreadAsync(
        Guid notificationId, Guid userId, CancellationToken cancellationToken = default)
    {
        var notification = await dbContext.UserNotifications
            .Include(n => n.User)
            .FirstOrDefaultAsync(n => n.Id == notificationId && n.User.Id == userId.ToString() && !n.IsDeleted, cancellationToken);

        if (notification is null)
            return Result<bool>.Error(ServiceErrorMessages.Notification.NotFoundOrForbidden);

        notification.IsRead = false;
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> MarkAllAsReadAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var notifications = await dbContext.UserNotifications
            .Include(n => n.User)
            .Where(n => n.User.Id == userId.ToString() && !n.IsRead && !n.IsDeleted)
            .ToListAsync(cancellationToken);

        foreach (var notification in notifications)
            notification.IsRead = true;

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> DeleteNotificationAsync(
        Guid notificationId, Guid userId, CancellationToken cancellationToken = default)
    {
        var notification = await dbContext.UserNotifications
            .Include(n => n.User)
            .FirstOrDefaultAsync(n => n.Id == notificationId && n.User.Id == userId.ToString() && !n.IsDeleted, cancellationToken);

        if (notification is null)
            return Result<bool>.Error(ServiceErrorMessages.Notification.NotFoundOrForbidden);

        notification.IsDeleted = true;
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }

    // ─── Admin ───────────────────────────────────────────────────────────────

    public async Task<Result<PagedResponse<UserNotificationDto>>> GetAllNotificationsAsync(
        int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        var query = dbContext.UserNotifications
            .Include(n => n.User)
            .Where(n => !n.IsDeleted)
            .OrderByDescending(n => n.CreatedOn);

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var mapped = mapper.Map<List<UserNotificationDto>>(items);
        return Result<PagedResponse<UserNotificationDto>>.Success(
            new PagedResponse<UserNotificationDto>(mapped, totalCount, pageNumber, pageSize));
    }

    public async Task<Result<UserNotificationDto>> GetNotificationByIdAdminAsync(
        Guid notificationId, CancellationToken cancellationToken = default)
    {
        var notification = await dbContext.UserNotifications
            .Include(n => n.User)
            .FirstOrDefaultAsync(n => n.Id == notificationId && !n.IsDeleted, cancellationToken);

        if (notification is null)
            return Result<UserNotificationDto>.Error(ServiceErrorMessages.Notification.NotFoundOrForbidden);

        return Result<UserNotificationDto>.Success(mapper.Map<UserNotificationDto>(notification));
    }

    public async Task<Result<bool>> AdminMarkAsReadAsync(
        Guid notificationId, CancellationToken cancellationToken = default)
    {
        var notification = await dbContext.UserNotifications
            .FirstOrDefaultAsync(n => n.Id == notificationId && !n.IsDeleted, cancellationToken);

        if (notification is null)
            return Result<bool>.Error(ServiceErrorMessages.Notification.NotFoundOrForbidden);

        notification.IsRead = true;
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> AdminDeleteNotificationAsync(
        Guid notificationId, CancellationToken cancellationToken = default)
    {
        var notification = await dbContext.UserNotifications
            .FirstOrDefaultAsync(n => n.Id == notificationId && !n.IsDeleted, cancellationToken);

        if (notification is null)
            return Result<bool>.Error(ServiceErrorMessages.Notification.NotFoundOrForbidden);

        notification.IsDeleted = true;
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}
