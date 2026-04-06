using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.CarInterior.Commands.CreateCarInterior;
using CarRentalZaimi.Application.Features.CarInterior.Commands.DeleteCarInterior;
using CarRentalZaimi.Application.Features.CarInterior.Commands.UpdateCarInterior;
using CarRentalZaimi.Application.Features.CarInterior.Queries.GetAllCarInteriorColor;

namespace CarRentalZaimi.Application.Interfaces.Services;

public interface ICarInteriorColorService
{
    Task<Result<CarInteriorColorDto>> CreateAsync(CreateCarInteriorColorCommand request, CancellationToken cancellationToken = default);
    Task<Result<CarInteriorColorDto>> UpdateAsync(UpdateCarInteriorColorCommand request, CancellationToken cancellationToken = default);
    Task<Result<bool>> DeleteAsync(DeleteCarInteriorColorCommand request, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<CarInteriorColorDto>>> GetAllAsync(GetAllCarInteriorColorQuery request, CancellationToken cancellationToken = default);
}
