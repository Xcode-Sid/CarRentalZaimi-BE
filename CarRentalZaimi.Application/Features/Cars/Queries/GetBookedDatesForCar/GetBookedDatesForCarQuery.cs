using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;

namespace CarRentalZaimi.Application.Features.Cars.Queries.GetBookedDatesForCar;

public class GetBookedDatesForCarQuery : IQuery<IEnumerable<BookedDateRangeDto>>
{
    public string? CarId { get; set; }
}
