using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Interfaces.Query;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.Notifications.Queries.GetUnreadCount;

internal class GetUnreadCountQueryHandler(INotificationQueryService _notificationQueryService)
    : IQueryHandler<GetUnreadCountQuery, int>
{
    public async Task<Result<int>> Handle(GetUnreadCountQuery request, CancellationToken cancellationToken)
        => await _notificationQueryService.GetUnreadCountAsync(request.UserId, cancellationToken);
}
