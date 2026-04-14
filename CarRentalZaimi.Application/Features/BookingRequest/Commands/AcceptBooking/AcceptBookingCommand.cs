using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.BookingRequest.Commands.AcceptBooking;

public class AcceptBookingCommand : ICommand<BookingDto>
{
    public string? BookingId { get; set; }
}
