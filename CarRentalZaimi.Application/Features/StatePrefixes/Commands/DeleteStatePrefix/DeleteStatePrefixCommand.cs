using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.StatePrefixes.Commands.DeleteStatePrefix;

public class DeleteStatePrefixCommand() : ICommand<bool>
{
    public string? Id { get; set; }
}