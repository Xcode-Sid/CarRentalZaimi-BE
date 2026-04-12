using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.BookingRequest.Queries.GetAllUserBookings;

internal class GetAllUserBookingsQueryHandler(IBookingServices _bookingService) : IQueryHandler<GetAllUserBookingsQuery, IEnumerable<BookingDto>>
{
    public async Task<Result<IEnumerable<BookingDto>>> Handle(GetAllUserBookingsQuery request, CancellationToken cancellationToken)
        => await _bookingService.GetAllUserBookingsAsync(request, cancellationToken);

}
