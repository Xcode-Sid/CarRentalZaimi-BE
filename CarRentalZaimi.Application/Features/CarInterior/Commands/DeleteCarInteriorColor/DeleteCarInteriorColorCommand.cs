using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.CarInterior.Commands.DeleteCarInterior;

public class DeleteCarInteriorColorCommand() : ICommand<bool>
{
    public string? Id { get; set; }
}
