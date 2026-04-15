using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;

namespace CarRentalZaimi.Application.Features.CreateOccupiedCarDays.Queries.GetCarCalendarData;

public class GetCarCalendarDataQuery : IQuery<IEnumerable<OccupiedCarDaysDto>>
{
    public string? CarId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
