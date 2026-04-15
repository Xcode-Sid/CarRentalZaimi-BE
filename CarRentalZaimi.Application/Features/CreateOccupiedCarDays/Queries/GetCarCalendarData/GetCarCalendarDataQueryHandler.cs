using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.CreateOccupiedCarDays.Queries.GetCarCalendarData;

internal class GetCarCalendarDataQueryHandler(IOccupiedCarDaysService _adsService) : IQueryHandler<GetCarCalendarDataQuery, IEnumerable<OccupiedCarDaysDto>>
{
    public async Task<Result<IEnumerable<OccupiedCarDaysDto>>> Handle(GetCarCalendarDataQuery request, CancellationToken cancellationToken)
        => await _adsService.GetCarCalendarDataAsync(request, cancellationToken);
}

