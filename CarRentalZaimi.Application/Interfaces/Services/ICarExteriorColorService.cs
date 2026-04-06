using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.CarExteriorColor.Commands.CreateCarExteriorColor;
using CarRentalZaimi.Application.Features.CarExteriorColor.Commands.DeleteCarExteriorColor;
using CarRentalZaimi.Application.Features.CarExteriorColor.Commands.UpdateCarExteriorColor;
using CarRentalZaimi.Application.Features.CarExteriorColor.Queries;
using CarRentalZaimi.Application.Features.CarInterior.Queries.GetAllCarInteriorColor;

namespace CarRentalZaimi.Application.Interfaces.Services;

public interface ICarExteriorColorService
{
    Task<Result<CarExteriorColorDto>> CreateAsync(CreateCarExteriorColorCommand request, CancellationToken cancellationToken = default);
    Task<Result<CarExteriorColorDto>> UpdateAsync(UpdateCarExteriorColorCommand request, CancellationToken cancellationToken = default);
    Task<Result<bool>> DeleteAsync(DeleteCarExteriorColorCommand request, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<CarExteriorColorDto>>> GetAllAsync(GetAllCarExteriorColorQuery request, CancellationToken cancellationToken = default);
}
