namespace CarRentalZaimi.Infrastructure.Seed;

public class DatabaseSeeder
{
    public static async Task SeedAllAsync(IServiceProvider services)
    {
        await RoleSeeder.SeedAsync(services);        
        await UserSeeder.SeedAsync(services);
        await CarCategorySeeder.SeedAsync(services);
    }
}
