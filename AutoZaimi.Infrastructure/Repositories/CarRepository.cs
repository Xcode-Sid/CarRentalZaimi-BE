using CarRentalZaimi.Application.Interfaces.Repositories;
using CarRentalZaimi.Domain.Entities;
using CarRentalZaimi.Infrastructure.Persistence;

namespace CarRentalZaimi.Infrastructure.Repositories;

public class CarRepository : ICarRepository
{
    private readonly ApplicationDbContext _context;

    public CarRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Car car, CancellationToken cancellationToken = default)
        => await _context.Cars.AddAsync(car, cancellationToken);
}
