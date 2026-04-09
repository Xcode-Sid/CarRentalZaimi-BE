using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.AdditionalService.Commands.UpdateAdditionalService;

internal class UpdateAdditionalServiceCommandHandler(IAdditionalServicesService _aditionalServicesService) : ICommandHandler<UpdateAdditionalServiceCommand, AdditionalServiceDto>
{
    public async Task<Result<AdditionalServiceDto>> Handle(UpdateAdditionalServiceCommand request, CancellationToken cancellationToken)
        => await _aditionalServicesService.UpdateAsync(request, cancellationToken);
}
