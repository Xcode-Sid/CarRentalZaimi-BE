using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.DTOs.Google;

namespace CarRentalZaimi.Application.Interfaces.Services;

public interface IGoogleOAuthService
{
    Task<ApiResponse<GoogleUserProfileResponse>> VerifyAuthorizationCodeAsync(string code, string redirectUri);
}
