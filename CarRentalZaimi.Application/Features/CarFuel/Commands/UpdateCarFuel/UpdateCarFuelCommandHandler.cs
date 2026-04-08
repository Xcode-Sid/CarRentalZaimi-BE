using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.CarFuel.Commands.UpdateCarFuel;

internal class UpdateCarFuelCommandHandler(ICarFuelService _carFuelService) : ICommandHandler<UpdateCarFuelCommand, CarFuelDto>
{
    public async Task<ApiResponse<CarFuelDto>> Handle(UpdateCarFuelCommand request, CancellationToken cancellationToken)
        => await _carFuelService.UpdateAsync(request, cancellationToken);
}

