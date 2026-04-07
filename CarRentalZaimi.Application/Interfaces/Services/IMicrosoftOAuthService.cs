using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.DTOs.Microsoft;

namespace CarRentalZaimi.Application.Interfaces.Services;

public interface IMicrosoftOAuthService
{
    Task<ApiResponse<MicrosoftUserProfileResponse>> VerifyAuthorizationCodeAsync(string code, string codeVerifier, string redirectUri);
}
