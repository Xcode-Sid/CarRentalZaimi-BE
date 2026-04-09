using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.SavedCar.Command.SaveCar;
using CarRentalZaimi.Application.Features.SavedCar.Queries;

namespace CarRentalZaimi.Application.Interfaces.Services;

public interface ISavedCarService
{
    Task<Result<bool>> SaveCarAsync(SaveCarCommand request, CancellationToken cancellationToken = default);
    Task<Result<PagedResponse<SavedCarDto>>> GetAllSavedCarsAsync(GetAllSavedCarsQuery request, CancellationToken cancellationToken = default);
}
