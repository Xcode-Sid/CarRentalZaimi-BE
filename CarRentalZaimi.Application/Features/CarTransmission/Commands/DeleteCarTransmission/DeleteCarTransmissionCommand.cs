using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.CarTransmission.Commands.DeleteCarTransmission;

public class DeleteCarTransmissionCommand() : ICommand<bool>
{
    public string? Id { get; set; }
}
