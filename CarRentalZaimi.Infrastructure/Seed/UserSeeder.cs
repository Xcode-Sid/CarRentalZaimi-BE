using CarRentalZaimi.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace CarRentalZaimi.Infrastructure.Seed;

public static class UserSeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        var userManager = services.GetRequiredService<UserManager<User>>();

        await SeedAdminAsync(userManager);
        await SeedCustomerAsync(userManager);
    }

    private static async Task SeedAdminAsync(UserManager<User> userManager)
    {
        var adminEmail = "admin@carrental.com";
        if (await userManager.FindByEmailAsync(adminEmail) is not null) return;

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
            await userManager.AddToRoleAsync(admin, "Admin");
    }

    private static async Task SeedCustomerAsync(UserManager<User> userManager)
    {
        var customerEmail = "customer01@carrental.com";
        if (await userManager.FindByEmailAsync(customerEmail) is not null) return;

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
            await userManager.AddToRoleAsync(customer, "Customer");
    }
}