using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.AdditionalService.Commands.DeleteAdditionalService;

internal class DeleteAdditionalServiceCommandHandler(IAdditionalServicesService _aditionalServicesService) : ICommandHandler<DeleteAdditionalServiceCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteAdditionalServiceCommand request, CancellationToken cancellationToken)
        => await _aditionalServicesService.DeleteAsync(request, cancellationToken);
}
