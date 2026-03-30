using CarRentalZaimi.Application.Interfaces.Repositories;

namespace CarRentalZaimi.Application.Interfaces.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    ICarRepository Cars { get; }
    IUserRepository Users { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}