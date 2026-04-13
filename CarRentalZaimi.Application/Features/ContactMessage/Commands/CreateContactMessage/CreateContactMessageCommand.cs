using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.ContactMessage.Commands.CreateContactMessage;

public class CreateContactMessageCommand : ICommand<bool>
{
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Subject { get; set; }
    public string? Message { get; set; }
}
