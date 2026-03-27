using CarRentalZaimi.Application.Interfaces.Repositories;
using CarRentalZaimi.Application.Interfaces.Services;
using CarRentalZaimi.Application.Interfaces.UnitOfWork;
using CarRentalZaimi.Infrastructure.Persistence;
using CarRentalZaimi.Infrastructure.Persistence.UnitOfWork;
using CarRentalZaimi.Infrastructure.Repositories;
using CarRentalZaimi.Infrastructure.Services;
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

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICarRepository, CarRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICarService, CarService>();

        return services;
    }
}