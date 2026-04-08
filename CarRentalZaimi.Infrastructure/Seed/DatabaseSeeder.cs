using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CarRentalZaimi.Logging;

namespace CarRentalZaimi.Infrastructure.Seed;

public class DatabaseSeeder
{
    public static async Task SeedAllAsync(IServiceProvider services)
    {
        var logger = services.GetRequiredService<ILoggerFactory>()
            .CreateLogger(nameof(DatabaseSeeder));

        logger.Info("Starting database seeding");

        await RoleSeeder.SeedAsync(services);
        await UserSeeder.SeedAsync(services);
        await CarCategorySeeder.SeedAsync(services);

        logger.Info("Database seeding completed");
    }
}

