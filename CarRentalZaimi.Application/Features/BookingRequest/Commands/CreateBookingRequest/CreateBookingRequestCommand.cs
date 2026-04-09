using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.BookingRequest.Commands.CreateBookingRequest;

public class CreateBookingRequestCommand : ICommand<BookingDto>
{
    public string? UserId { get; set; }
    public string? CarId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalPrice { get; set; }
    public string? PhoneNumber { get; set; }
    public string? PaymentMethod { get; set; }
    public ICollection<string>? AditionalServiceIds { get; set; }
}
