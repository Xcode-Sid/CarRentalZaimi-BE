using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.CarFuel.Commands.DeleteCarFuel;

public class DeleteCarFuelCommand() : ICommand<bool>
{
    public string? Id { get; set; }
}
