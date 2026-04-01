using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;

namespace CarRentalZaimi.Application.Interfaces.Services;

public interface IAuthenticationService
{
    Task<Result<UserDto>> RegisterAsync(string firstname, string lastname, DateTime? dateOfBirth, 
        string username, string email, string phone, string password, string? name, string? data, string? role, string? deviceInfo = null);
    Task<Result<AuthenticationResponseDto>> AuthenticateWithGoogleAsync(string? email, string? firstName, string? lastName, string? picture, 
        string? externalProviderId, string userType, string? deviceInfo = null, CancellationToken cancellationToken = default);
    Task<Result<AuthenticationResponseDto>> AuthenticateWithFacebookAsync(string? email, string? firstName, string? lastName, string? picture, 
        string? externalProviderId, string userType, string? deviceInfo = null, CancellationToken cancellationToken = default);
    Task<Result<AuthenticationResponseDto>> AuthenticateWithMicrosoftAsync(string? email, string? firstName, string? lastName, 
        string? externalProviderId, string userType, string? deviceInfo = null, CancellationToken cancellationToken = default);
    Task<Result<AuthenticationResponseDto>> AuthenticateWithYahooAsync(string? email, string? firstName, string? lastName, string? picture, 
        string? externalProviderId, string userType, string? deviceInfo = null, CancellationToken cancellationToken = default);
}
