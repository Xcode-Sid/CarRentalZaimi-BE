using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.CarInterior.Commands.UpdateCarInterior;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.CarInterior.Commands.UpdateCarInteriorColor;

internal class UpdateCarInteriorColorCommandHandler(ICarInteriorColorService _carInteriorColorService) : ICommandHandler<UpdateCarInteriorColorCommand, CarInteriorColorDto>
{
    public async Task<Result<CarInteriorColorDto>> Handle(UpdateCarInteriorColorCommand request, CancellationToken cancellationToken)
        => await _carInteriorColorService.UpdateAsync(request, cancellationToken);
}
