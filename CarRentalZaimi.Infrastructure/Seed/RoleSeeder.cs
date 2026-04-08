using CarRentalZaimi.Domain.Entities;
using CarRentalZaimi.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CarRentalZaimi.Logging;

namespace CarRentalZaimi.Infrastructure.Seed;

public static class RoleSeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        var logger = services.GetRequiredService<ILoggerFactory>()
            .CreateLogger(nameof(RoleSeeder));
        var roleManager = services.GetRequiredService<RoleManager<Role>>();

        foreach (var role in Enum.GetValues<UserRole>())
        {
            var roleName = role.ToString();
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new Role(roleName)
                {
                    CreatedOn = DateTime.UtcNow,
                    Description = role switch
                    {
                        UserRole.Admin => "Full platform access",
                        UserRole.User => "Can browse and book cars",
                        _ => roleName
                    }
                });
                logger.Info("Seeded role: {RoleName}", roleName);
            }
        }
    }
}

