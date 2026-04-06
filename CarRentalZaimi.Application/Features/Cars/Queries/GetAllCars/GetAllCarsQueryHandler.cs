using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.Cars.Queries.GetAllCars;

internal class GetAllCarsQueryHandler(ICarService _carService) : IQueryHandler<GetAllCarsQuery, PagedResponse<CarDto>>
{
    public async Task<Result<PagedResponse<CarDto>>> Handle(GetAllCarsQuery request, CancellationToken cancellationToken)
        => await _carService.GetAllCarsAsync(request, cancellationToken);
}
