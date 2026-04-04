using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.Phone.Commands.ConfirmPhone;

public record ConfirmPhoneCommand(string UserId, string Token) : ICommand<bool>;