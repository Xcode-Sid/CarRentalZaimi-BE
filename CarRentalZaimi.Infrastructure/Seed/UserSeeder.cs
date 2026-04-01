using CarRentalZaimi.Domain.Entities;
using CarRentalZaimi.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CarRentalZaimi.Infrastructure.Seed;

public static class UserSeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        var logger = services.GetRequiredService<ILoggerFactory>()
            .CreateLogger(nameof(UserSeeder));
        var userManager = services.GetRequiredService<UserManager<User>>();

        await SeedAdminAsync(userManager, logger);
        await SeedCustomerAsync(userManager, logger);
    }

    private static async Task SeedAdminAsync(UserManager<User> userManager, ILogger logger)
    {
        var adminEmail = "admin@carrental.com";

        if (await userManager.FindByEmailAsync(adminEmail) is not null)
            return;

        var admin = new User
        {
            FirstName = "System",
            LastName = "Admin",
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true,
            PhoneNumber = "+355691234567",
            DateOfBirth = new DateTime(1990, 1, 1),
            CreatedOn = DateTime.UtcNow,
        };

        var result = await userManager.CreateAsync(admin, "Admin@123!");

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(admin, nameof(UserRole.Admin));
            logger.LogInformation("Seeded admin user: {Email}", adminEmail);
        }
        else
        {
            logger.LogWarning("Failed to seed admin user: {Errors}",
                string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }

    private static async Task SeedCustomerAsync(UserManager<User> userManager, ILogger logger)
    {
        var customerEmail = "customer01@carrental.com";

        if (await userManager.FindByEmailAsync(customerEmail) is not null)
            return;

        var customer = new User
        {
            FirstName = "customer",
            LastName = "01",
            UserName = customerEmail,
            Email = customerEmail,
            EmailConfirmed = true,
            PhoneNumber = "+355697654321",
            DateOfBirth = new DateTime(1995, 6, 15),
            CreatedOn = DateTime.UtcNow,
        };

        var result = await userManager.CreateAsync(customer, "Customer@123!");

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(customer, nameof(UserRole.User));
            logger.LogInformation("Seeded customer user: {Email}", customerEmail);
        }
        else
        {
            logger.LogWarning("Failed to seed customer user: {Errors}",
                string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }
}
