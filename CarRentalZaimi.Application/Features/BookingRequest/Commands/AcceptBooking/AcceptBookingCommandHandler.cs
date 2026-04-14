using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.BookingRequest.Commands.AcceptBooking;

public class AcceptBookingCommandHandler(IBookingServices _bookingService) : ICommandHandler<AcceptBookingCommand, BookingDto>
{
    public async Task<Result<BookingDto>> Handle(AcceptBookingCommand request, CancellationToken cancellationToken)
        => await _bookingService.AcceptBookingRequestAsync(request, cancellationToken);

}
