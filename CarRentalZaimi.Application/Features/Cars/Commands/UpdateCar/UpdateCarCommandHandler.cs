using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.Cars.Commands.UpdateCar;

internal class UpdateCarCommandHandler(ICarService _carService) : ICommandHandler<UpdateCarCommand, CarDto>
{
    public async Task<Result<CarDto>> Handle(UpdateCarCommand request, CancellationToken cancellationToken)
        => await _carService.UpdateCarAsync(request, cancellationToken);
}
