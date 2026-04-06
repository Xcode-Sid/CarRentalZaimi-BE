using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.CarFuel.Commands.CreateCarFuel;

public class CreateCarFuelCommandHandler(ICarFuelService _carFuelService) : ICommandHandler<CreateCarFuelCommand, CarFuelDto>
{
    public async Task<Result<CarFuelDto>> Handle(CreateCarFuelCommand request, CancellationToken cancellationToken)
        => await _carFuelService.CreateAsync(request, cancellationToken);
}
