using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.Cars.Commands.AddFeaturedCar;
using CarRentalZaimi.Application.Features.Cars.Commands.CreateCar;
using CarRentalZaimi.Application.Features.Cars.Commands.DeleteCar;
using CarRentalZaimi.Application.Features.Cars.Commands.UpdateCar;
using CarRentalZaimi.Application.Features.Cars.Queries.GetAllCars;
using CarRentalZaimi.Application.Features.Cars.Queries.GetBookedDatesForCar;
using CarRentalZaimi.Application.Features.Cars.Queries.GetCarById;
using CarRentalZaimi.Application.Features.Cars.Queries.GetFeaturedCars;

namespace CarRentalZaimi.Application.Interfaces.Services;

public interface ICarService
{
    Task<Result<CarDto>> CreateCarAsync(CreateCarCommand request, CancellationToken cancellationToken = default);
    Task<Result<CarDto>> UpdateCarAsync(UpdateCarCommand request, CancellationToken cancellationToken = default);
    Task<Result<bool>> DeleteCarAsync(DeleteCarCommand request, CancellationToken cancellationToken = default);
    Task<Result<CarDto>> GetCarByIdAsync(GetCarByIdQuery request, CancellationToken cancellationToken = default);
    Task<Result<PagedResponse<CarDto>>> GetAllPagedCarsAsync(GetAllPagedCarsQuery request, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<CarDto>>> GetAllCarsAsync(GetAllCarsQuery request, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<CarDto>>> GetFeaturedCarsAsync(GetFeaturedCarsQuery request, CancellationToken cancellationToken = default);
    Task<Result<bool>> AddFeaturedCarAsync(AddFeaturedCarCommand request, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<BookedDateRangeDto>>> GetBookedDatesForCarAsync(GetBookedDatesForCarQuery request, CancellationToken cancellationToken = default);
}