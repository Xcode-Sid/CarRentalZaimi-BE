using CarRentalZaimi.Application.Interfaces.Repositories;
using CarRentalZaimi.Domain.Entities;
using CarRentalZaimi.Infrastructure.Persistence;
using Microsoft.Extensions.Logging;
using CarRentalZaimi.Logging;

namespace CarRentalZaimi.Infrastructure.Repositories;

public class CarRepository(
    ApplicationDbContext _context,
    ILogger<CarRepository> _logger) : ICarRepository
{
    public async Task AddAsync(Car car, CancellationToken cancellationToken = default)
    {
        _logger.Debug("Adding car to database: {LicensePlate}", car.LicensePlate);
        await _context.Cars.AddAsync(car, cancellationToken);
    }
}
