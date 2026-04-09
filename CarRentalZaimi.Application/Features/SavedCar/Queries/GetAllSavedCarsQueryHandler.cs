using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.SavedCar.Queries;

internal class GetAllSavedCarsQueryHandler(ISavedCarService _savedCarService) : IQueryHandler<GetAllSavedCarsQuery, PagedResponse<SavedCarDto>>
{
    public async Task<Result<PagedResponse<SavedCarDto>>> Handle(GetAllSavedCarsQuery request, CancellationToken cancellationToken)
        => await _savedCarService.GetAllSavedCarsAsync(request, cancellationToken);
}

