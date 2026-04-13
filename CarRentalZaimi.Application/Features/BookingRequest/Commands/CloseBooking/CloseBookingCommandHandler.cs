using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.BookingRequest.Commands.CloseBooking;

internal class CloseBookingCommandHandler(IBookingServices _bookingService) : ICommandHandler<CloseBookingCommand, BookingDto>
{
    public async Task<Result<BookingDto>> Handle(CloseBookingCommand request, CancellationToken cancellationToken)
        => await _bookingService.CloseBookingRequestAsync(request, cancellationToken);

}
