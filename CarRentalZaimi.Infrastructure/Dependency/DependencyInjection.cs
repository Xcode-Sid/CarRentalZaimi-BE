using CarRentalZaimi.Application.Common.Constants;
using CarRentalZaimi.Application.Common.Email;
using CarRentalZaimi.Application.Interfaces.Repositories;
using CarRentalZaimi.Application.Interfaces.Services;
using CarRentalZaimi.Application.Interfaces.UnitOfWork;
using CarRentalZaimi.Application.Services;
using CarRentalZaimi.Domain.Entities;
using CarRentalZaimi.Infrastructure.Identity;
using CarRentalZaimi.Infrastructure.Persistence;
using CarRentalZaimi.Infrastructure.Persistence.UnitOfWork;
using CarRentalZaimi.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
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

        services.AddIdentity<User, Role>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.AddHttpContextAccessor();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICarRepository, CarRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.Configure<EmailSettings>(configuration.GetSection(ConfigurationKeys.Sections.Email));
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IPasswordResetService, PasswordResetService>();

        return services;
    }
}