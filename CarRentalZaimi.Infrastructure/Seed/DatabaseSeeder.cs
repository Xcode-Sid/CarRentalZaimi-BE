using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CarRentalZaimi.Infrastructure.Seed;

public class DatabaseSeeder
{
    public static async Task SeedAllAsync(IServiceProvider services)
    {
        var logger = services.GetRequiredService<ILoggerFactory>()
            .CreateLogger(nameof(DatabaseSeeder));

        logger.LogInformation("Starting database seeding");

        await RoleSeeder.SeedAsync(services);
        await UserSeeder.SeedAsync(services);
        await CarCategorySeeder.SeedAsync(services);

        logger.LogInformation("Database seeding completed");
    }
}
