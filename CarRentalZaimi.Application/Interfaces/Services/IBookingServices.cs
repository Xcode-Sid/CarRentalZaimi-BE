using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.BookingRequest.Commands.AcceptBooking;
using CarRentalZaimi.Application.Features.BookingRequest.Commands.CancelBooking;
using CarRentalZaimi.Application.Features.BookingRequest.Commands.CreateBookingRequest;
using CarRentalZaimi.Application.Features.BookingRequest.Commands.RefuseBooking;
using CarRentalZaimi.Application.Features.BookingRequest.Queries.GetAllBookings;
using CarRentalZaimi.Application.Features.BookingRequest.Queries.GetAllUserBookings;

namespace CarRentalZaimi.Application.Interfaces.Services;

public interface IBookingServices
{
    Task<Result<BookingDto>> CreateBookingRequestAsync(CreateBookingRequestCommand request, CancellationToken cancellationToken = default);
    Task<Result<PagedResponse<BookingDto>>> GetAllBookingsAsync(GetAllBookingsQuery request, CancellationToken cancellationToken = default);
    Task<Result<BookingDto>> AcceptBookingRequestAsync(AcceptBookingCommand request, CancellationToken cancellationToken = default);
    Task<Result<BookingDto>> RefuseBookingRequestAsync(RefuseBookingCommand request, CancellationToken cancellationToken = default);
    Task<Result<bool>> CancelBookingAsync(CancelBookingCommand request, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<BookingDto>>> GetAllUserBookingsAsync(GetAllUserBookingsQuery request, CancellationToken cancellationToken = default);
}

