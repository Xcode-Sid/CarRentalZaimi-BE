using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Features.CarExteriorColor.Commands.CreateCarExteriorColor;
using CarRentalZaimi.Application.Features.CarExteriorColor.Commands.DeleteCarExteriorColor;
using CarRentalZaimi.Application.Features.CarExteriorColor.Commands.UpdateCarExteriorColor;
using CarRentalZaimi.Application.Features.CarExteriorColor.Queries;

namespace CarRentalZaimi.Application.Interfaces.Services;

public interface ICarExteriorColorService
{
    Task<ApiResponse<CarExteriorColorDto>> CreateAsync(CreateCarExteriorColorCommand request, CancellationToken cancellationToken = default);
    Task<ApiResponse<CarExteriorColorDto>> UpdateAsync(UpdateCarExteriorColorCommand request, CancellationToken cancellationToken = default);
    Task<ApiResponse<bool>> DeleteAsync(DeleteCarExteriorColorCommand request, CancellationToken cancellationToken = default);
    Task<ApiResponse<IEnumerable<CarExteriorColorDto>>> GetAllAsync(GetAllCarExteriorColorQuery request, CancellationToken cancellationToken = default);
}
