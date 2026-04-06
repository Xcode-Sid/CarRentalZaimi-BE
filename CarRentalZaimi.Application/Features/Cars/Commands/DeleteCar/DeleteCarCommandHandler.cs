using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.Cars.Commands.DeleteCar;

internal class DeleteCarCommandHandler(ICarService _carService) : ICommandHandler<DeleteCarCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteCarCommand request, CancellationToken cancellationToken)
        => await _carService.DeleteCarAsync(request, cancellationToken);
}
