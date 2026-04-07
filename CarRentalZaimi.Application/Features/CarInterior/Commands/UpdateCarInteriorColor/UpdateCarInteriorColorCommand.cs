using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.CarInterior.Commands.UpdateCarInterior;

public record UpdateCarInteriorColorCommand() : ICommand<CarInteriorColorDto>
{
    public string? Id { get; set; }
    public string? Name { get; set; }
}
