using AutoMapper;
using CarRentalZaimi.Application.Behaviors;
using CarRentalZaimi.Application.Dependency;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CarRentalZaimi.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddApplicationCommon();

        // Add MediatR
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);
        });

        // Add MediatR Pipeline Behaviors
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

        return services;
    }
}

