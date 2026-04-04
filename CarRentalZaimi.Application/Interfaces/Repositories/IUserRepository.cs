using CarRentalZaimi.Domain.Entities;

namespace CarRentalZaimi.Application.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);
    Task AddRefreshTokenAsync(RefreshToken refreshToken);
    Task<IReadOnlyList<RefreshToken>> GetRefreshTokensByUserIdAsync(string userId);
    Task UpdateRefreshTokensAsync(IReadOnlyList<RefreshToken> tokens);
}
