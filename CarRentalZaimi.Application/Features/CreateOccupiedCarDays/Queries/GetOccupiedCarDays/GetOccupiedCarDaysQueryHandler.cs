using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.CreateOccupiedCarDays.Queries.GetOccupiedCarDays;

internal class GetOccupiedCarDaysQueryHandler(IOccupiedCarDaysService _adsService) : IQueryHandler<GetOccupiedCarDaysQuery, PagedResponse<OccupiedCarDaysDto>>
{
    public async Task<Result<PagedResponse<OccupiedCarDaysDto>>> Handle(GetOccupiedCarDaysQuery request, CancellationToken cancellationToken)
        => await _adsService.GetGetOccupiedCarDaysAsync(request, cancellationToken);
}

