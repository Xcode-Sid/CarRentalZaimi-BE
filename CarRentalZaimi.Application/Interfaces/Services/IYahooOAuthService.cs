using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs.Yahoo;

namespace CarRentalZaimi.Application.Interfaces.Services;

public interface IYahooOAuthService
{
    Task<Result<YahooUserProfileResponse>> VerifyAuthorizationCodeAsync(string code, string codeVerifier, string redirectUri);
}

