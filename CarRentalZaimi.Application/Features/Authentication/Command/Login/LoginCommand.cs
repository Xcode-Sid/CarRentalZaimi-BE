using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.Authentication.Command.Login;

public record LoginCommand(string Login, string Password) : ICommand<AuthenticationResponseDto>;
