using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;

namespace CarRentalZaimi.Application.Features.BookingRequest.Queries.GetAllUserBookings;

public class GetAllUserBookingsQuery : IQuery<IEnumerable<BookingDto>>
{ 
    public string? UserId { get; set; }
}
