using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.CarTransmission.Commands.UpdateCarTransmission;

internal class UpdateCarTransmissionCommandHandler(ICarTransmissionService _carTransmissionService) : ICommandHandler<UpdateCarTransmissionCommand, CarTransmissionDto>
{
    public async Task<ApiResponse<CarTransmissionDto>> Handle(UpdateCarTransmissionCommand request, CancellationToken cancellationToken)
        => await _carTransmissionService.UpdateAsync(request, cancellationToken);
}

