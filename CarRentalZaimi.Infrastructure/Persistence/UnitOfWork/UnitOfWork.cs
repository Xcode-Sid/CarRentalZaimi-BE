using CarRentalZaimi.Application.Interfaces.Repositories;
using CarRentalZaimi.Application.Interfaces.UnitOfWork;

namespace CarRentalZaimi.Infrastructure.Persistence.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public ICarRepository Cars { get; }
    public IUserRepository Users { get; }

    public UnitOfWork(ApplicationDbContext context, ICarRepository cars, IUserRepository users)
    {
        _context = context;
        Cars = cars;
        Users = users;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => await _context.SaveChangesAsync(cancellationToken);

    public void Dispose() => _context.Dispose();
}