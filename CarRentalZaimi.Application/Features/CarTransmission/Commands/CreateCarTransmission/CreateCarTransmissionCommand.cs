using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.CarTransmission.Commands.CreateCarTransmission;

public class CreateCarTransmissionCommand() : ICommand<CarTransmissionDto>
{
    public string? Name { get; set; }
}
