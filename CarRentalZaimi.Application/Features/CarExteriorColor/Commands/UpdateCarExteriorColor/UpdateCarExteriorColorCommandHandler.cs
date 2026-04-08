using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.CarExteriorColor.Commands.UpdateCarExteriorColor;

internal class UpdateCarExteriorColorCommandHandler(ICarExteriorColorService _carExteriorColorService) : ICommandHandler<UpdateCarExteriorColorCommand, CarExteriorColorDto>
{
    public async Task<ApiResponse<CarExteriorColorDto>> Handle(UpdateCarExteriorColorCommand request, CancellationToken cancellationToken)
        => await _carExteriorColorService.UpdateAsync(request, cancellationToken);
}