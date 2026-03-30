using CarRentalZaimi.Application.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace CarRentalZaimi.Infrastructure.Repositories;

public class UserRepository(ILogger<UserRepository> _logger) : IUserRepository
{
}
