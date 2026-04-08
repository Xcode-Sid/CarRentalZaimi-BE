using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.DTOs.ApiResponse;

namespace CarRentalZaimi.Application.Interfaces.Services;

public interface IAuthenticationService
{
    Task<ApiResponse<UserDto>> RegisterAsync(string firstname, string lastname, DateTime? dateOfBirth,
        string username, string email, string phone, string password, string? name, string? data, string? role, string? location, string? deviceInfo = null);
    Task<ApiResponse<AuthenticationResponseDto>> AuthenticateWithGoogleAsync(string? email, string? firstName, string? lastName, string? picture,
        string? externalProviderId, string? deviceInfo = null, CancellationToken cancellationToken = default);
    Task<ApiResponse<AuthenticationResponseDto>> AuthenticateWithFacebookAsync(string? email, string? firstName, string? lastName, string? picture,
        string? externalProviderId, string? deviceInfo = null, CancellationToken cancellationToken = default);
    Task<ApiResponse<AuthenticationResponseDto>> AuthenticateWithMicrosoftAsync(string? email, string? firstName, string? lastName,
        string? externalProviderId, string? deviceInfo = null, CancellationToken cancellationToken = default);
    Task<ApiResponse<AuthenticationResponseDto>> AuthenticateWithYahooAsync(string? email, string? firstName, string? lastName, string? picture,
        string? externalProviderId, string? deviceInfo = null, CancellationToken cancellationToken = default);
    Task<ApiResponse<AuthenticationResponseDto>> LoginAsync(string login, string password, string? ipAddress = null, string? deviceInfo = null);
    Task<ApiResponse<bool>> LogoutAsync(string userId);
    Task<ApiResponse<bool>> ChangePasswordAsync(string userId, string currentPassword, string newPassword);
}
