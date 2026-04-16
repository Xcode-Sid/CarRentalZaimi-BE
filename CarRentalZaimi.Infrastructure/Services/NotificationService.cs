using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Interfaces.Services;
using CarRentalZaimi.Domain.Entities;
using CarRentalZaimi.Domain.Enums;
using CarRentalZaimi.Infrastructure.Hubs;
using CarRentalZaimi.Infrastructure.Persistence;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace CarRentalZaimi.Infrastructure.Services;

public class NotificationService(
    IHubContext<NotificationHub> hubContext,
    ApplicationDbContext dbContext) : INotificationService
{
    public async Task SendNotificationToUserAsync(Guid userId, string message, UserNotificationType type)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId.ToString());
        if (user == null) return;

        var notification = new UserNotification
        {
            User = user,
            Message = message,
            UserNotificationType = type,
            IsRead = false
        };

        dbContext.UserNotifications.Add(notification);
        await dbContext.SaveChangesAsync();

        await hubContext.Clients.User(userId.ToString()).SendAsync("ReceiveNotification", new
        {
            notification.Id,
            notification.Message,
            notification.UserNotificationType,
            notification.CreatedOn,
            notification.IsRead
        });
    }

    public async Task<Result<bool>> SendNotificationToAdminsAsync(string message, UserNotificationType type)
    {
        try
        {
            // Find all users who are in the Admin role
            var adminUsers = await dbContext.Users
                .Where(u => dbContext.UserRoles
                    .Any(ur => ur.UserId == u.Id && dbContext.Roles
                        .Any(r => r.Id == ur.RoleId && r.Name == UserRole.Admin.ToString())))
                .ToListAsync();

            if (!adminUsers.Any())
                return Result<bool>.Success(true);

            var notifications = new List<UserNotification>();
            foreach (var admin in adminUsers)
            {
                notifications.Add(new UserNotification
                {
                    Id = Guid.NewGuid(),
                    User = admin,
                    Message = message,
                    UserNotificationType = type,
                    IsRead = false
                });
            }

            await dbContext.UserNotifications.AddRangeAsync(notifications);
            await dbContext.SaveChangesAsync();

            // Broadcast via SignalR to each admin
            foreach (var notification in notifications)
            {
                await hubContext.Clients.User(notification.User.Id).SendAsync("ReceiveNotification", new
                {
                    notification.Id,
                    notification.Message,
                    notification.UserNotificationType,
                    notification.CreatedOn,
                    notification.IsRead
                });
            }

            return Result<bool>.Success(true);
        }
        catch (Exception)
        {
            return Result<bool>.Error("Failed to send admin notifications");
        }
    }

    public async Task SendNotificationToAllAsync(string message, UserNotificationType type)
    {
        var users = await dbContext.Users.ToListAsync();
        var notifications = new List<UserNotification>();
        foreach (var user in users)
        {
            var notification = new UserNotification
            {
                User = user,
                Message = message,
                UserNotificationType = type,
                IsRead = false
            };
            notifications.Add(notification);
            dbContext.UserNotifications.Add(notification);
        }
        await dbContext.SaveChangesAsync();

        foreach (var notification in notifications)
        {
            await hubContext.Clients.User(notification.User.Id).SendAsync("ReceiveNotification", new
            {
                notification.Id,
                notification.Message,
                notification.UserNotificationType,
                notification.CreatedOn,
                notification.IsRead
            });
        }
    }
}
