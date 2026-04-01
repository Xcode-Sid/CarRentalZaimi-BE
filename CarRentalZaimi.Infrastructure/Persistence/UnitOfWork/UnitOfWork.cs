using CarRentalZaimi.Application.Interfaces.Repositories;
using CarRentalZaimi.Application.Interfaces.UnitOfWork;
using Microsoft.Extensions.Logging;

namespace CarRentalZaimi.Infrastructure.Persistence.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<UnitOfWork> _logger;

    public ICarRepository Cars { get; }
    public IUserRepository Users { get; }

    public UnitOfWork(
        ApplicationDbContext context,
        ICarRepository cars,
        IUserRepository users,
        ILogger<UnitOfWork> logger)
    {
        _context = context;
        Cars = cars;
        Users = users;
        _logger = logger;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Saving changes to database");
        var count = await _context.SaveChangesAsync(cancellationToken);
        _logger.LogDebug("Saved {Count} change(s) to database", count);
        return count;
    }

    public void Dispose() => _context.Dispose();
}