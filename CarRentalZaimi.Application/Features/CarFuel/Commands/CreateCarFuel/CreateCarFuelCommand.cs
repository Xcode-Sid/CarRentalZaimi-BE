using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.CarFuel.Commands.CreateCarFuel;

public class CreateCarFuelCommand() : ICommand<CarFuelDto>
{
    public string? Name { get; set; }
}
