using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.DTOs.Yahoo;

namespace CarRentalZaimi.Application.Interfaces.Services;

public interface IYahooOAuthService
{
    Task<ApiResponse<YahooUserProfileResponse>> VerifyAuthorizationCodeAsync(string code, string codeVerifier, string redirectUri);
}
