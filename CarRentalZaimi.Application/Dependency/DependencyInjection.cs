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
        services.AddScoped<ICarFuelService, CarFuelService>();
        services.AddScoped<ICarTransmissionService, CarTransmissionService>();
        services.AddScoped<ICarInteriorColorService, CarInteriorColorService>();
        services.AddScoped<ICarExteriorColorService, CarExteriorColorService>();
        services.AddScoped<ICarCategoryService, CarCategoryService>();
        services.AddScoped<ICarCompanyNameService, CarCompanyNameService>();
        services.AddScoped<ICarCompanyModelService, CarCompanyModelService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IPhoneService, PhoneService>();
        services.AddScoped<IPromotionService, PromotionService>();
        services.AddScoped<IAdditionalServicesService, AdditionalServicesService>();
        services.AddScoped<ICarReviewService, CarReviewService>();
        services.AddScoped<ISavedCarService, SavedCarService>();
        services.AddScoped<IBookingServices, BookingServices>();
        services.AddScoped<ICompanyProfileService, CompanyProfileService>();
        services.AddScoped<IPartnerService, PartnerService>();
        return services;
    }
}