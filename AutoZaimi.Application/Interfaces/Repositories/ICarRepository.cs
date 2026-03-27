using CarRentalZaimi.Domain.Entities;

namespace CarRentalZaimi.Application.Interfaces.Repositories;

public interface ICarRepository
{
    Task AddAsync(Car car, CancellationToken cancellationToken = default);
}
