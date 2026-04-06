using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.Phone.Commands.SendVerificationCode;

public class SendVerificationCodeCommand() : ICommand<bool>
{
    public required string UserId { get; set; }
}
