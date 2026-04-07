using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.CarFuel.Commands.UpdateCarFuel;

public record UpdateCarFuelCommand() : ICommand<CarFuelDto>
{
    public string? Id { get; set; }
    public string? Name { get; set; }
}
