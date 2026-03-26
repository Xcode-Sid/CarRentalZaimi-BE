using CarRentalZaimi.Application.Interfaces.Repositories;
using CarRentalZaimi.Infrastructure.Persistence;
using CarRentalZaimi.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarRentalZaimi.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySQL( 
                configuration.GetConnectionString("Default")!
            ));

        services.AddScoped<ICarRepository, CarRepository>();

        return services;
    }
}