using AutoZaimi.Domain.Entities;

namespace AutoZaimi.Application.Interfaces.Repositories;

public interface ICarRepository
{
    Task<Car> AddAsync(Car car, CancellationToken ct);
}
