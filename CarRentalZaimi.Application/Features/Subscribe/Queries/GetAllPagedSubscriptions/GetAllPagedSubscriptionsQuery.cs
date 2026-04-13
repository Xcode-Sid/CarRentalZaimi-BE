using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;

namespace CarRentalZaimi.Application.Features.Subscribe.Queries.GetAllPagedSubscriptions;

public class GetAllPagedSubscriptionsQuery : IQuery<PagedResponse<SubscribeDto>>
{
    public string? Search { get; set; }
    public int PageNr { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
