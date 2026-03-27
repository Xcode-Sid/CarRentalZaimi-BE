using CarRentalZaimi.Application.Mappings;
using Microsoft.Extensions.DependencyInjection;

namespace CarRentalZaimi.Application.Dependency;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile).Assembly);
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
        return services;
    }
}