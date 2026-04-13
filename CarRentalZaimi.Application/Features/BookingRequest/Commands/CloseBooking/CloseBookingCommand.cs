using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.BookingRequest.Commands.CloseBooking;

public class CloseBookingCommand : ICommand<BookingDto>
{
    public string? BookingId { get; set; }
}