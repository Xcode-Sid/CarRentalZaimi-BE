using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.Privacy.Commands.DeletePrivacy;

public class DeletePrivacyCommand : ICommand<bool>
{
    public string? Id { get; set; }
}
