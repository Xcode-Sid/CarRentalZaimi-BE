using CarRentalZaimi.Application.Interfaces.Services;
using CarRentalZaimi.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CarRentalZaimi.Application.Dependency;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationCommon(this IServiceCollection services)
    {

        // Add error service
        services.AddScoped<IErrorService, ErrorService>();
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<IUserContext, UserContext>();
        services.AddScoped<IUserContext, UserContext>();
        services.AddScoped<ICarService, CarService>();
        services.AddScoped<IDeviceDetectorService, DeviceDetectorService>();
        services.AddScoped<IStatePrefixService, StatePrefixService>();
        services.AddScoped<IUserService, UserService>();
        return services;
    }
}