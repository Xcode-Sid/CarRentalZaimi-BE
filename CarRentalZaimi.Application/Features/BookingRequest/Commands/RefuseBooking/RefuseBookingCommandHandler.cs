using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.BookingRequest.Commands.RefuseBooking;

internal class RefuseBookingCommandHandler(IBookingServices _bookingService) : ICommandHandler<RefuseBookingCommand, BookingDto>
{
    public async Task<Result<BookingDto>> Handle(RefuseBookingCommand request, CancellationToken cancellationToken)
        => await _bookingService.RefuseBookingRequestAsync(request, cancellationToken);

}
