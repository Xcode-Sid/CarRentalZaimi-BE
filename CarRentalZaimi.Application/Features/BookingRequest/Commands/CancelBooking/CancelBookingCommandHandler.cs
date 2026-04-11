using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.BookingRequest.Commands.CancelBooking;

internal class CancelBookingCommandHandler(IBookingServices _bookingService) : ICommandHandler<CancelBookingCommand, bool>
{
    public async Task<Result<bool>> Handle(CancelBookingCommand request, CancellationToken cancellationToken)
        => await _bookingService.CancelBookingAsync(request, cancellationToken);

}

