using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.CarFuel.Commands.DeleteCarFuel;

internal class DeleteCarFuelCommandHandler(ICarFuelService _carFuelService) : ICommandHandler<DeleteCarFuelCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteCarFuelCommand request, CancellationToken cancellationToken)
        => await _carFuelService.DeleteAsync(request, cancellationToken);
}
