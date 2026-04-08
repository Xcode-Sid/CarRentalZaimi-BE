using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Features.CarInterior.Commands.CreateCarInterior;
using CarRentalZaimi.Application.Features.CarInterior.Commands.DeleteCarInterior;
using CarRentalZaimi.Application.Features.CarInterior.Commands.UpdateCarInterior;
using CarRentalZaimi.Application.Features.CarInterior.Queries.GetAllCarInteriorColor;

namespace CarRentalZaimi.Application.Interfaces.Services;

public interface ICarInteriorColorService
{
    Task<ApiResponse<CarInteriorColorDto>> CreateAsync(CreateCarInteriorColorCommand request, CancellationToken cancellationToken = default);
    Task<ApiResponse<CarInteriorColorDto>> UpdateAsync(UpdateCarInteriorColorCommand request, CancellationToken cancellationToken = default);
    Task<ApiResponse<bool>> DeleteAsync(DeleteCarInteriorColorCommand request, CancellationToken cancellationToken = default);
    Task<ApiResponse<IEnumerable<CarInteriorColorDto>>> GetAllAsync(GetAllCarInteriorColorQuery request, CancellationToken cancellationToken = default);
}
