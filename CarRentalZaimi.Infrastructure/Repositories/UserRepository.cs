using CarRentalZaimi.Application.Interfaces.Repositories;
using CarRentalZaimi.Domain.Entities;
using CarRentalZaimi.Domain.Enums;
using CarRentalZaimi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CarRentalZaimi.Infrastructure.Repositories;

public class UserRepository(ApplicationDbContext context) : Repository<User>(context), IUserRepository
{
    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .FirstOrDefaultAsync(u => u.Email == email && !u.IsDeleted, cancellationToken);
    }

    public async Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .FirstOrDefaultAsync(u => u.UserName == username && !u.IsDeleted, cancellationToken);
    }

    public async Task<User?> GetByEmailWithProfilesAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .FirstOrDefaultAsync(u => u.Email == email && !u.IsDeleted, cancellationToken);
    }

    public async Task<IReadOnlyList<User>> GetUsersByStatusAsync(UserStatus userStatus, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(u => u.Status == userStatus && !u.IsDeleted)
            .OrderBy(u => u.UserName)
            .ToListAsync(cancellationToken);
    }


    public async Task<IReadOnlyList<User>> GetVerifiedUsersAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(u => u.EmailConfirmed && !u.IsDeleted)
            .OrderBy(u => u.UserName)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<User>> SearchUsersAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return new List<User>();

        var term = searchTerm.ToLower();
        return await _dbSet
            .Where(u => !u.IsDeleted && (u.UserName!.ToLower().Contains(term) ||
                       u.Email!.ToLower().Contains(term)))
            .OrderBy(u => u.UserName)
            .Take(50)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> IsEmailUniqueAsync(string email, Guid? excludeUserId = null, CancellationToken cancellationToken = default)
    {
        var query = _dbSet.Where(u => u.Email == email && !u.IsDeleted);

        if (excludeUserId.HasValue)
            query = query.Where(u => u.Id != excludeUserId.Value.ToString());

        return !await query.AnyAsync(cancellationToken);
    }

    public async Task<int> GetUserCountByStatusAsync(UserStatus userStatus, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .CountAsync(u => u.Status == userStatus && !u.IsDeleted, cancellationToken);
    }

    public async Task<IReadOnlyList<User>> GetRecentlyRegisteredUsersAsync(int days = 7, CancellationToken cancellationToken = default)
    {
        var cutoffDate = DateTime.UtcNow.AddDays(-days);
        return await _dbSet
            .Where(u => u.CreatedOn >= cutoffDate && !u.IsDeleted)
            .OrderByDescending(u => u.CreatedOn)
            .ToListAsync(cancellationToken);
    }

    public override async Task<User?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default)
    {
        if (id is string stringId)
            return await _dbSet
                .FirstOrDefaultAsync(u => u.Id == stringId && !u.IsDeleted, cancellationToken);
        
        return await base.GetByIdAsync(id, cancellationToken);
    }

    public async Task<IReadOnlyList<RefreshToken>> GetRefreshTokensByUserIdAsync(string userId)
    {
        return await context.RefreshTokens
            .Where(rt => rt.User != null && rt.User.Id == userId)
            .ToListAsync();
    }

    public async Task UpdateRefreshTokensAsync(IReadOnlyList<RefreshToken> tokens)
    {
        foreach (var token in tokens)
        {
            context.RefreshTokens.Update(token);
        }

        await context.SaveChangesAsync();
    }

    public async Task<RefreshToken?> GetRefreshTokenByTokenAsync(string token)
    {
        return await context.RefreshTokens
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.Token == token);
    }

    public async Task AddRefreshTokenAsync(RefreshToken refreshToken)
    {
        await context.RefreshTokens.AddAsync(refreshToken);
        await context.SaveChangesAsync();
    }
}
