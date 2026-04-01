using CarRentalZaimi.Application.Interfaces.Services;
using CarRentalZaimi.Application.Interfaces.UnitOfWork;
using CarRentalZaimi.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace CarRentalZaimi.Infrastructure.Services;

public class CarService(
    IUnitOfWork _uow,
    ILogger<CarService> _logger) : ICarService
{
    public async Task<Car> CreateCarAsync(Car car, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Creating car: {LicensePlate}", car.LicensePlate);

        await _uow.Cars.AddAsync(car, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Car created successfully: {LicensePlate}", car.LicensePlate);
        return car;
    }
}