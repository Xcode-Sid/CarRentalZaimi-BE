using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.Cars.Commands.DeleteCar;

public class DeleteCarCommand(string? id) : ICommand<bool>
{
    public string? Id { get; } = id;
}
