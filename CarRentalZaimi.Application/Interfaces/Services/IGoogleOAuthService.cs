using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs.Google;

namespace CarRentalZaimi.Application.Interfaces.Services;

public interface IGoogleOAuthService
{
    Task<Result<GoogleUserProfileResponse>> VerifyAuthorizationCodeAsync(string code, string redirectUri);
}
