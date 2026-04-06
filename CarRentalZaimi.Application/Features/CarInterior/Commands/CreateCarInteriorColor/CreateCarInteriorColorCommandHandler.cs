using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.CarInterior.Commands.CreateCarInterior;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.CarInterior.Commands.CreateCarInteriorColor;

public class CreateCarInteriorColorCommandHAndler(ICarInteriorColorService _carInteriorColorService) : ICommandHandler<CreateCarInteriorColorCommand, CarInteriorColorDto>
{
    public async Task<Result<CarInteriorColorDto>> Handle(CreateCarInteriorColorCommand request, CancellationToken cancellationToken)
        => await _carInteriorColorService.CreateAsync(request, cancellationToken);
}
