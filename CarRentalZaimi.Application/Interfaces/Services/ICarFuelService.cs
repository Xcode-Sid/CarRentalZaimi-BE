using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Features.CarFuel.Commands.CreateCarFuel;
using CarRentalZaimi.Application.Features.CarFuel.Commands.DeleteCarFuel;
using CarRentalZaimi.Application.Features.CarFuel.Commands.UpdateCarFuel;
using CarRentalZaimi.Application.Features.CarFuel.Queries.GetAllCarFuels;

namespace CarRentalZaimi.Application.Interfaces.Services;

public interface ICarFuelService
{
    Task<ApiResponse<CarFuelDto>> CreateAsync(CreateCarFuelCommand request, CancellationToken cancellationToken = default);
    Task<ApiResponse<CarFuelDto>> UpdateAsync(UpdateCarFuelCommand request, CancellationToken cancellationToken = default);
    Task<ApiResponse<bool>> DeleteAsync(DeleteCarFuelCommand request, CancellationToken cancellationToken = default);
    Task<ApiResponse<IEnumerable<CarFuelDto>>> GetAllAsync(GetAllCarFuelsQuery request, CancellationToken cancellationToken = default);
}
