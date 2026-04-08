using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Interfaces.Query;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.CarInterior.Queries.GetAllCarInteriorColor;

internal class GetAllCarInteriorColorQueryHandler(ICarInteriorColorService _carInteriorColorService) : IQueryHandler<GetAllCarInteriorColorQuery, IEnumerable<CarInteriorColorDto>>
{
    public async Task<ApiResponse<IEnumerable<CarInteriorColorDto>>> Handle(GetAllCarInteriorColorQuery request, CancellationToken cancellationToken)
        => await _carInteriorColorService.GetAllAsync(request, cancellationToken);
}