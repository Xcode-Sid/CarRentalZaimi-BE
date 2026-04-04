using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.Authentication.Logout;

public record LogoutCommand(string UserId) : ICommand<bool>;
