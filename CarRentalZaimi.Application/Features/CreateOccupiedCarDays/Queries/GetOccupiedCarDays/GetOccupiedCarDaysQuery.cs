using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;

namespace CarRentalZaimi.Application.Features.CreateOccupiedCarDays.Queries.GetOccupiedCarDays;

public class GetOccupiedCarDaysQuery : IQuery<PagedResponse<OccupiedCarDaysDto>>
{
    public string? CarId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int PageNr { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
