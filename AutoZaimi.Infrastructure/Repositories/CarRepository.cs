using AutoZaimi.Application.Interfaces.Repositories;
using AutoZaimi.Domain.Entities;
using AutoZaimi.Infrastructure.Persistence;

namespace AutoZaimi.Infrastructure.Repositories;

public class CarRepository(ApplicationDbContext _context) : ICarRepository
{
    public Task<Car> AddAsync(Car car, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
