using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.CarExteriorColor.Commands.CreateCarExteriorColor;

public class CreateCarExteriorColorCommand() : ICommand<CarExteriorColorDto>
{
    public string? Name { get; set; }
}
