using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.Terms.Commands.DeleteTerm;

public class DeleteTermCommand : ICommand<bool>
{
    public string? Id { get; set; }
}
