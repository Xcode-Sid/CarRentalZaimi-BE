using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.BookingRequest.Commands.CreateBookingRequest;

internal class CreateBookingRequestCommandHandler(IBookingServices _bookingService) : ICommandHandler<CreateBookingRequestCommand, BookingDto>
{
    public async Task<Result<BookingDto>> Handle(CreateBookingRequestCommand request, CancellationToken cancellationToken)
        => await _bookingService.CreateBookingRequestAsync(request, cancellationToken);

}

