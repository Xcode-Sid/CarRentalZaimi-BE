using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.Authentication.Command.ResetPassword;

public record ResetPasswordCommand(string Email, string Token, string NewPassword) : ICommand<bool>;
