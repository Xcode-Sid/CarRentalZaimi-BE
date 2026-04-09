using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.Cars.Queries.GetAllCars;

internal class GetAllPagedCarsQueryHandler(ICarService _carService) : IQueryHandler<GetAllPagedCarsQuery, PagedResponse<CarDto>>
{
    public async Task<Result<PagedResponse<CarDto>>> Handle(GetAllPagedCarsQuery request, CancellationToken cancellationToken)
        => await _carService.GetAllPagedCarsAsync(request, cancellationToken);
}
