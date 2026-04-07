using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.CarExteriorColor.Commands.DeleteCarExteriorColor;

internal class DeleteCarExteriorColorCommandHandler(ICarExteriorColorService _carExteriorColorService) : ICommandHandler<DeleteCarExteriorColorCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteCarExteriorColorCommand request, CancellationToken cancellationToken)
        => await _carExteriorColorService.DeleteAsync(request, cancellationToken);
}