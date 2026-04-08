using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.CarTransmission.Commands.UpdateCarTransmission;

public record UpdateCarTransmissionCommand() : ICommand<CarTransmissionDto>
{
    public string? Id { get; set; }
    public string? Name { get; set; }
}
