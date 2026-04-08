using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.Cars.Queries.GetFeaturedCars;

internal class GetFeaturedCarsQueryHandler(ICarService _carService) : IQueryHandler<GetFeaturedCarsQuery, IEnumerable<CarDto>>
{
    public async Task<Result<IEnumerable<CarDto>>> Handle(GetFeaturedCarsQuery request, CancellationToken cancellationToken)
        => await _carService.GetFeaturedCarsAsync(request, cancellationToken);
}
