using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.Cars.Queries.GetBookedDatesForCar;

internal class GetBookedDatesForCarQueryHandler(ICarService _carService) : IQueryHandler<GetBookedDatesForCarQuery, IEnumerable<BookedDateRangeDto>>
{
    public async Task<Result<IEnumerable<BookedDateRangeDto>>> Handle(GetBookedDatesForCarQuery request, CancellationToken cancellationToken)
        => await _carService.GetBookedDatesForCarAsync(request, cancellationToken);
}
