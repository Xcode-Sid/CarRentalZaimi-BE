using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Interfaces.Query;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.CarFuel.Queries.GetAllCarFuels;

internal class GetAllCarFuelsQueryHandler(ICarFuelService _carFuelService) : IQueryHandler<GetAllCarFuelsQuery, IEnumerable<CarFuelDto>>
{
    public async Task<ApiResponse<IEnumerable<CarFuelDto>>> Handle(GetAllCarFuelsQuery request, CancellationToken cancellationToken)
        => await _carFuelService.GetAllAsync(request, cancellationToken);
}
