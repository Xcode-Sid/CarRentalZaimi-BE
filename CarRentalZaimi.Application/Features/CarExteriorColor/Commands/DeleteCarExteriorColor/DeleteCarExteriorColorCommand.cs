using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.CarExteriorColor.Commands.DeleteCarExteriorColor;

public class DeleteCarExteriorColorCommand() : ICommand<bool>
{
    public string? Id { get; set; }
}
