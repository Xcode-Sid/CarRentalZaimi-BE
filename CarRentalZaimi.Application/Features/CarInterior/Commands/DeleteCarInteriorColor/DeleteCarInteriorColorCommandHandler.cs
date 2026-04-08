using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Features.CarInterior.Commands.DeleteCarInterior;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.CarInterior.Commands.DeleteCarInteriorColor;

internal class DeleteCarInteriorColorCommandHandler(ICarInteriorColorService _carInteriorColorService) : ICommandHandler<DeleteCarInteriorColorCommand, bool>
{
    public async Task<ApiResponse<bool>> Handle(DeleteCarInteriorColorCommand request, CancellationToken cancellationToken)
        => await _carInteriorColorService.DeleteAsync(request, cancellationToken);
}