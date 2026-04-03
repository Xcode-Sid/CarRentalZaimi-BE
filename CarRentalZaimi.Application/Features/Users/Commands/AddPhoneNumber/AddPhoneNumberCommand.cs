
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.Users.Commands.AddPhoneNumber;

public record AddPhoneNumberCommand() : ICommand<UserDto>
{
    public string? UserId { get; set; }
    public string? PhoneNumber { get; set; }
}
