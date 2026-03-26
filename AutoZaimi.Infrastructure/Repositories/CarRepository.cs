using CarRentalZaimi.Application.Interfaces.Repositories;
using CarRentalZaimi.Domain.Entities;
using CarRentalZaimi.Infrastructure.Persistence;

namespace CarRentalZaimi.Infrastructure.Repositories;

public class CarRepository(ApplicationDbContext _context) : ICarRepository
{
    public Task<Car> AddAsync(Car car, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
