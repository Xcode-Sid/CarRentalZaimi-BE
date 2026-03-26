using CarRentalZaimi.Domain.Entities;

namespace CarRentalZaimi.Application.Interfaces.Repositories;

public interface ICarRepository
{
    Task<Car> AddAsync(Car car, CancellationToken ct);
}
