using CarRentalZaimi.API.Controllers.Base;
using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Common.Messages;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.Notifications.Commands.AdminDeleteNotification;
using CarRentalZaimi.Application.Features.Notifications.Commands.AdminMarkNotificationRead;
using CarRentalZaimi.Application.Features.Notifications.Commands.DeleteNotification;
using CarRentalZaimi.Application.Features.Notifications.Commands.MarkAllNotificationsRead;
using CarRentalZaimi.Application.Features.Notifications.Commands.MarkNotificationRead;
using CarRentalZaimi.Application.Features.Notifications.Commands.MarkNotificationUnread;
using CarRentalZaimi.Application.Features.Notifications.Queries.GetAdminNotificationById;
using CarRentalZaimi.Application.Features.Notifications.Queries.GetAdminNotifications;
using CarRentalZaimi.Application.Features.Notifications.Queries.GetUnreadCount;
using CarRentalZaimi.Application.Features.Notifications.Queries.GetUserNotificationById;
using CarRentalZaimi.Application.Features.Notifications.Queries.GetUserNotifications;
using CarRentalZaimi.Domain.Common.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalZaimi.API.Controllers;

[Authorize]
public class NotificationController(IMediator mediator) : ApiControllerBase(mediator)
{

    [HttpGet(Name = nameof(GetMyNotifications))]
    [ProducesResponseType(typeof(Result<PagedResponse<UserNotificationDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetMyNotifications(
        [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var (userId, unauthorizedResult) = GetCurrentUserIdOrUnauthorized();
        if (unauthorizedResult is not null) return unauthorizedResult;

        return await SendQuery(new GetUserNotificationsQuery
        {
            UserId = Guid.Parse(userId!),
            PageNumber = pageNumber,
            PageSize = pageSize
        });
    }

    [HttpGet("{id}", Name = nameof(GetMyNotificationById))]
    [ProducesResponseType(typeof(Result<UserNotificationDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetMyNotificationById([FromRoute] Guid id)
    {
        var (userId, unauthorizedResult) = GetCurrentUserIdOrUnauthorized();
        if (unauthorizedResult is not null) return unauthorizedResult;

        return await SendQuery(new GetUserNotificationByIdQuery
        {
            NotificationId = id,
            UserId = Guid.Parse(userId!)
        });
    }

    [HttpGet("unread-count", Name = nameof(GetUnreadCount))]
    [ProducesResponseType(typeof(Result<int>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetUnreadCount()
    {
        var (userId, unauthorizedResult) = GetCurrentUserIdOrUnauthorized();
        if (unauthorizedResult is not null) return unauthorizedResult;

        return await SendQuery(new GetUnreadCountQuery { UserId = Guid.Parse(userId!) });
    }

    [HttpPatch("{id}/read", Name = nameof(MarkNotificationRead))]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> MarkNotificationRead([FromRoute] Guid id)
    {
        var (userId, unauthorizedResult) = GetCurrentUserIdOrUnauthorized();
        if (unauthorizedResult is not null) return unauthorizedResult;

        return await SendCommand(new MarkNotificationReadCommand
        {
            NotificationId = id,
            UserId = Guid.Parse(userId!)
        }, SuccessMessages.Notification.NotificationMarkedRead);
    }

    [HttpPatch("{id}/unread", Name = nameof(MarkNotificationUnread))]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> MarkNotificationUnread([FromRoute] Guid id)
    {
        var (userId, unauthorizedResult) = GetCurrentUserIdOrUnauthorized();
        if (unauthorizedResult is not null) return unauthorizedResult;

        return await SendCommand(new MarkNotificationUnreadCommand
        {
            NotificationId = id,
            UserId = Guid.Parse(userId!)
        }, SuccessMessages.Notification.NotificationMarkedUnread);
    }

    [HttpPatch("read-all", Name = nameof(MarkAllNotificationsRead))]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> MarkAllNotificationsRead()
    {
        var (userId, unauthorizedResult) = GetCurrentUserIdOrUnauthorized();
        if (unauthorizedResult is not null) return unauthorizedResult;

        return await SendCommand(
            new MarkAllNotificationsReadCommand { UserId = Guid.Parse(userId!) },
            SuccessMessages.Notification.AllNotificationsMarkedRead);
    }

    [HttpDelete("{id}", Name = nameof(DeleteNotification))]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteNotification([FromRoute] Guid id)
    {
        var (userId, unauthorizedResult) = GetCurrentUserIdOrUnauthorized();
        if (unauthorizedResult is not null) return unauthorizedResult;

        return await SendCommand(new DeleteNotificationCommand
        {
            NotificationId = id,
            UserId = Guid.Parse(userId!)
        }, SuccessMessages.Notification.NotificationDeleted);
    }


    [HttpGet("admin", Name = nameof(GetAllNotificationsAdmin))]
    [Authorize(SystemPolicies.Admin)]
    [ProducesResponseType(typeof(Result<PagedResponse<UserNotificationDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetAllNotificationsAdmin(
        [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        return await SendQuery(new GetAdminNotificationsQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        });
    }

    [HttpGet("admin/{id}", Name = nameof(GetNotificationByIdAdmin))]
    [Authorize(SystemPolicies.Admin)]
    [ProducesResponseType(typeof(Result<UserNotificationDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetNotificationByIdAdmin([FromRoute] Guid id)
    {
        return await SendQuery(new GetAdminNotificationByIdQuery { NotificationId = id });
    }

    [HttpPatch("admin/{id}/read", Name = nameof(AdminMarkNotificationRead))]
    [Authorize(SystemPolicies.Admin)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> AdminMarkNotificationRead([FromRoute] Guid id)
    {
        return await SendCommand(
            new AdminMarkNotificationReadCommand { NotificationId = id },
            SuccessMessages.Notification.NotificationMarkedRead);
    }

    [HttpDelete("admin/{id}", Name = nameof(AdminDeleteNotification))]
    [Authorize(SystemPolicies.Admin)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> AdminDeleteNotification([FromRoute] Guid id)
    {
        return await SendCommand(
            new AdminDeleteNotificationCommand { NotificationId = id },
            SuccessMessages.Notification.NotificationDeleted);
    }
}
