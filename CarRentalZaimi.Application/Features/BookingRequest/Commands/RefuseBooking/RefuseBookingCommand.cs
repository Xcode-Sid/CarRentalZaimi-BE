using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.BookingRequest.Commands.RefuseBooking;

public class RefuseBookingCommand : ICommand<BookingDto>
{
    public string? BookingId { get; set; }
    public string? RefusedReanson { get; set; }
}
