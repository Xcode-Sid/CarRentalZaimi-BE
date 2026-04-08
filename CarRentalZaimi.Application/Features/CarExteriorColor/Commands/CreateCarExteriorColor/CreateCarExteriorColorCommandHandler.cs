using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.CarExteriorColor.Commands.CreateCarExteriorColor;

internal class CreateCarExteriorColorCommandHandler(ICarExteriorColorService _carExteriorColorService) : ICommandHandler<CreateCarExteriorColorCommand, CarExteriorColorDto>
{
    public async Task<ApiResponse<CarExteriorColorDto>> Handle(CreateCarExteriorColorCommand request, CancellationToken cancellationToken)
        => await _carExteriorColorService.CreateAsync(request, cancellationToken);
}