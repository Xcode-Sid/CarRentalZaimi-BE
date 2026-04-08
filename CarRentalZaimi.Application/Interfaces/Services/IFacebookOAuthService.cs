using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.DTOs.Facebook;

namespace CarRentalZaimi.Application.Interfaces.Services;

public interface IFacebookOAuthService
{
    Task<ApiResponse<FacebookUserProfileResponse>> VerifyAuthorizationCodeAsync(string code, string redirectUri);
}
