using CarRentalZaimi.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace CarRentalZaimi.Infrastructure.Seed;

public static class RoleSeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        var roleManager = services.GetRequiredService<RoleManager<Role>>();

        string[] roles = ["Admin", "User"];
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new Role(role)
                {
                    CreatedOn = DateTime.UtcNow,
                    Description = role == "Admin"
                        ? "Full platform access"
                        : "Can browse and book cars"
                });
            }
        }
    }
}
