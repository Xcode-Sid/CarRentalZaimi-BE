using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs.Facebook;

namespace CarRentalZaimi.Application.Interfaces.Services;

public interface IFacebookOAuthService
{
    Task<Result<FacebookUserProfileResponse>> VerifyAuthorizationCodeAsync(string code, string redirectUri);
}
