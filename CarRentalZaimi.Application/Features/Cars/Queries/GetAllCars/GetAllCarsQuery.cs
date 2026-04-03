using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;

namespace CarRentalZaimi.Application.Features.Cars.Queries.GetAllCars;

public class GetAllCarsQuery : IQuery<PagedResponse<CarDto>>
{
    public string? Search { get; set; }
    public int PageNr { get; set; }
    public int PageSize { get; set; }
}
