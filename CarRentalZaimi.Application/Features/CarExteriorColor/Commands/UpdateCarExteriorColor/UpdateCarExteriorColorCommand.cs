using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.CarExteriorColor.Commands.UpdateCarExteriorColor;

public record UpdateCarExteriorColorCommand() : ICommand<CarExteriorColorDto>
{
    public string? Id { get; set; }
    public string? Name { get; set; }
}
