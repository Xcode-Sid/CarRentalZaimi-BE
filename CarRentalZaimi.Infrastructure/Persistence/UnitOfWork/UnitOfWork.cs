using CarRentalZaimi.Application.Interfaces.Repositories;
using CarRentalZaimi.Application.Interfaces.UnitOfWork;
using CarRentalZaimi.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;

namespace CarRentalZaimi.Infrastructure.Persistence.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<UnitOfWork> _logger;
    private readonly Dictionary<Type, object> _repositories = new(); 

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

    public IRepository<T> Repository<T>() where T : class
    {
        var type = typeof(T);
        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(Repository<>).MakeGenericType(type);
            var repositoryInstance = Activator.CreateInstance(repositoryType, _context);
            _repositories.Add(type, repositoryInstance!);
        }
        return (IRepository<T>)_repositories[type];
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