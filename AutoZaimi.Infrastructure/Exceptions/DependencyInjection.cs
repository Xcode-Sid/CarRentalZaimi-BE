using AutoZaimi.Application.Interfaces.Repositories;
using AutoZaimi.Infrastructure.Persistence;
using AutoZaimi.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AutoZaimi.Infrastructure;

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