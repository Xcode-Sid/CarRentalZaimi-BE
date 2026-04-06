using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.Cars.Commands.CreateCar;

public class CreateCarCommandHandler(ICarService _carService) : ICommandHandler<CreateCarCommand, CarDto>
{
    public async Task<Result<CarDto>> Handle(CreateCarCommand request, CancellationToken cancellationToken)
        => await _carService.CreateCarAsync(request, cancellationToken);
}
