using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.CarExteriorColor.Queries;

internal class GetAllCarExteriorColorQueryHandler(ICarExteriorColorService _carExteriorColorService) : IQueryHandler<GetAllCarExteriorColorQuery, IEnumerable<CarExteriorColorDto>>
{
    public async Task<Result<IEnumerable<CarExteriorColorDto>>> Handle(GetAllCarExteriorColorQuery request, CancellationToken cancellationToken)
        => await _carExteriorColorService.GetAllAsync(request, cancellationToken);
}