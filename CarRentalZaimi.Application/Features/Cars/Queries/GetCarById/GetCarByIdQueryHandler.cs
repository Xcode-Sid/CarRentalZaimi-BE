using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.Cars.Queries.GetCarById;

internal class GetCarByIdQueryHandler(ICarService _carService) : IQueryHandler<GetCarByIdQuery, CarDto>
{
    public async Task<Result<CarDto>> Handle(GetCarByIdQuery request, CancellationToken cancellationToken)
        => await _carService.GetCarByIdAsync(request, cancellationToken);
}
