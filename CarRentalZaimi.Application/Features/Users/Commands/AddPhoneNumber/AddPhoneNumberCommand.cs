
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.Users.Commands.AddPhoneNumber;

public class AddPhoneNumberCommand() : ICommand<UserDto>
{
    public string? UserId { get; set; }
    public string? PhoneNumber { get; set; }
}
