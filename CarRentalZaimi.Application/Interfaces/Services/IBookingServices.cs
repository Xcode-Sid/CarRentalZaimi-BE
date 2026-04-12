using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.BookingRequest.Commands.CreateBookingRequest;
using CarRentalZaimi.Application.Features.BookingRequest.Queries.GetAllBookings;
using CarRentalZaimi.Application.Features.BookingRequest.Queries.GetAllUserBookings;

namespace CarRentalZaimi.Application.Interfaces.Services;

public interface IBookingServices
{
    Task<Result<BookingDto>> CreateBookingRequestAsync(CreateBookingRequestCommand request, CancellationToken cancellationToken = default);
    Task<Result<PagedResponse<BookingDto>>> GetAllBookingsAsync(GetAllBookingsQuery request, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<BookingDto>>> GetAllUserBookingsAsync(GetAllUserBookingsQuery request, CancellationToken cancellationToken = default);

}
