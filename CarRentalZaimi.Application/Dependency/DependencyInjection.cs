using CarRentalZaimi.Application.Behaviors;
using CarRentalZaimi.Application.Mappings;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CarRentalZaimi.Application.Dependency;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        return services;
    }
}