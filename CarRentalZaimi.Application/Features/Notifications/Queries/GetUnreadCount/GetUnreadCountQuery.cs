using CarRentalZaimi.Application.Interfaces.Query;

namespace CarRentalZaimi.Application.Features.Notifications.Queries.GetUnreadCount;

public class GetUnreadCountQuery : IQuery<int>
{
    public Guid UserId { get; set; }
}
