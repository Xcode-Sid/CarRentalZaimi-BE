using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.CarTransmission.Commands.DeleteCarTransmission;

internal class DeleteCarTransmissionCommandHandler(ICarTransmissionService _carTransmissionService) : ICommandHandler<DeleteCarTransmissionCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteCarTransmissionCommand request, CancellationToken cancellationToken)
        => await _carTransmissionService.DeleteAsync(request, cancellationToken);
}
