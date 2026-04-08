using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.Authentication.Command.ChangePassword;

public record ChangePasswordCommand(string CurrentPassword, string NewPassword) : ICommand<bool>;
