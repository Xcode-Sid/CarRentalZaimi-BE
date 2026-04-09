using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.AdditionalService.Commands.CreateAdditionalService;

internal class CreateAdditionalServiceCommandHandler(IAdditionalServicesService _aditionalServicesService) : ICommandHandler<CreateAdditionalServiceCommand, AdditionalServiceDto>
{
    public async Task<Result<AdditionalServiceDto>> Handle(CreateAdditionalServiceCommand request, CancellationToken cancellationToken)
        => await _aditionalServicesService.CreateAsync(request, cancellationToken);
}
