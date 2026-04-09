using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.BookingRequest.Queries.GetAllBookings;

internal class GetAllBookingsQueryHandler(IBookingServices _bookingService) : IQueryHandler<GetAllBookingsQuery, PagedResponse<BookingDto>>
{
    public async Task<Result<PagedResponse<BookingDto>>> Handle(GetAllBookingsQuery request, CancellationToken cancellationToken)
        => await _bookingService.GetAllBookingsAsync(request, cancellationToken);

}
