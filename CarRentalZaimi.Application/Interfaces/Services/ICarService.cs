using CarRentalZaimi.Domain.Entities;

namespace CarRentalZaimi.Application.Interfaces.Services;

public interface ICarService
{
    Task<Car> CreateCarAsync(Car car, CancellationToken cancellationToken = default);
}