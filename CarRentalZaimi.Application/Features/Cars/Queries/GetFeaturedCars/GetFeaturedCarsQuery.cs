using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;

namespace CarRentalZaimi.Application.Features.Cars.Queries.GetFeaturedCars;

public class GetFeaturedCarsQuery : IQuery<IEnumerable<CarDto>>
{
    public int Limit { get; set; } = 10;
}
