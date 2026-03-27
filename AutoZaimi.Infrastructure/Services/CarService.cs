using CarRentalZaimi.Application.Interfaces.Services;
using CarRentalZaimi.Application.Interfaces.UnitOfWork;
using CarRentalZaimi.Domain.Entities;

namespace CarRentalZaimi.Infrastructure.Services;
public class CarService : ICarService
{
    private readonly IUnitOfWork _uow;

    public CarService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Car> CreateCarAsync(Car car, CancellationToken cancellationToken = default)
    {
        await _uow.Cars.AddAsync(car, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);
        return car;
    }
}