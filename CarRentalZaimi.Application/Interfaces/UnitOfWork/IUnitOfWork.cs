using CarRentalZaimi.Application.Interfaces.Repositories;

namespace CarRentalZaimi.Application.Interfaces.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    ICarRepository Cars { get; }
    IUserRepository Users { get; }
    IRepository<T> Repository<T>() where T : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}