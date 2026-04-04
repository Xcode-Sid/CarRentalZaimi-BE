using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.Authentication.Command.ForgotPassword;

public record ForgotPasswordCommand(string Email) : ICommand<string>;
