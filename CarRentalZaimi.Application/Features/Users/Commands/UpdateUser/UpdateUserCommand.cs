using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.Users.Commands.UpdateUser;

public class UpdateUserCommand : ICommand<UserDto>
{
    public string? UserId { get; init; }
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Name { get; set; }
    public string? Data { get; set; }
    public string? Location { get; set; }
}
