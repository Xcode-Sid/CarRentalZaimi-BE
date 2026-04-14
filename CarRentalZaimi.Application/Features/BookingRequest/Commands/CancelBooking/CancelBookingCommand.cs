using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.BookingRequest.Commands.CancelBooking;

public class CancelBookingCommand : ICommand<bool>
{
    public string? BookingId { get; set; }
    public string? Reason { get; set; }
}
