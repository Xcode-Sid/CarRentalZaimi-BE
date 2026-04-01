using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs.Microsoft;

namespace CarRentalZaimi.Application.Interfaces.Services;

public interface IMicrosoftOAuthService
{
    Task<Result<MicrosoftUserProfileResponse>> VerifyAuthorizationCodeAsync(string code, string codeVerifier, string redirectUri);
}

