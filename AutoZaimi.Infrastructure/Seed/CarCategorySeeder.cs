using CarRentalZaimi.Domain.Entities;
using CarRentalZaimi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CarRentalZaimi.Infrastructure.Seed;

public static class CarCategorySeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        var db = services.GetRequiredService<ApplicationDbContext>();

        if (await db.CarCategories.AnyAsync()) return; 

        var categories = new List<CarCategory>
        {
            new() { Name = "Economy",  Description = "Affordable everyday cars" },
            new() { Name = "SUV",      Description = "Spacious family vehicles" },
            new() { Name = "Luxury",   Description = "Premium comfort vehicles" },
            new() { Name = "Van",      Description = "Large group transport" },
            new() { Name = "Electric", Description = "Eco-friendly electric cars" },
        };

        await db.CarCategories.AddRangeAsync(categories);
        await db.SaveChangesAsync();
    }
}