using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Features.Cars.Commands.CreateCar;
using CarRentalZaimi.Application.Features.Cars.Commands.DeleteCar;
using CarRentalZaimi.Application.Features.Cars.Commands.UpdateCar;
using CarRentalZaimi.Application.Features.Cars.Queries.GetAllCars;
using CarRentalZaimi.Application.Features.Cars.Queries.GetCarById;

namespace CarRentalZaimi.Application.Interfaces.Services;

public interface ICarService
{
    Task<ApiResponse<CarDto>> CreateCarAsync(CreateCarCommand request, CancellationToken cancellationToken = default);
    Task<ApiResponse<CarDto>> UpdateCarAsync(UpdateCarCommand request, CancellationToken cancellationToken = default);
    Task<ApiResponse<bool>> DeleteCarAsync(DeleteCarCommand request, CancellationToken cancellationToken = default);
    Task<ApiResponse<CarDto>> GetCarByIdAsync(GetCarByIdQuery request, CancellationToken cancellationToken = default);
    Task<ApiResponse<PagedResponse<CarDto>>> GetAllCarsAsync(GetAllCarsQuery request, CancellationToken cancellationToken = default);
}