using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.CarInterior.Commands.CreateCarInterior;

public class CreateCarInteriorColorCommand() : ICommand<CarInteriorColorDto>
{
    public string? Name { get; set; }
}
