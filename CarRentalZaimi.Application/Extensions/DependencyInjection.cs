using AutoMapper;
using CarRentalZaimi.Application.Behaviors;
using CarRentalZaimi.Application.Dependency;
using CarRentalZaimi.Application.Mappings;
using CarRentalZaimi.Application.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace CarRentalZaimi.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddApplicationCommon();
        services.AddSingleton<IMapper>(sp =>
        {
            var loggerFactory = sp.GetRequiredService<ILoggerFactory>();
            var expression = new MapperConfigurationExpression();
            expression.AddProfile<MappingProfile>();
            var config = new MapperConfiguration(expression, loggerFactory);
            return config.CreateMapper();
        });

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

