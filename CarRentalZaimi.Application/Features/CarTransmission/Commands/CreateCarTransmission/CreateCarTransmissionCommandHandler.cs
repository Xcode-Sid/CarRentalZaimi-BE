using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.CarTransmission.Commands.CreateCarTransmission;

public class CreateCarTransmissionCommandHandler(ICarTransmissionService _carTransmissionService) : ICommandHandler<CreateCarTransmissionCommand, CarTransmissionDto>
{
    public async Task<Result<CarTransmissionDto>> Handle(CreateCarTransmissionCommand request, CancellationToken cancellationToken)
        => await _carTransmissionService.CreateAsync(request, cancellationToken);
}

