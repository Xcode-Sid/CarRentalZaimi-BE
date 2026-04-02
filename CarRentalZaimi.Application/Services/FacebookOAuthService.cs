using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Common.Errors;
using CarRentalZaimi.Application.Common.Facebook;
using CarRentalZaimi.Application.DTOs.Facebook;
using CarRentalZaimi.Application.Interfaces.Services;
using System.Text.Json;

namespace CarRentalZaimi.Application.Services;


public class FacebookOAuthService(
    HttpClient httpClient,
    FacebookOAuthSettings settings,
    IErrorService errorService) : IFacebookOAuthService
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly FacebookOAuthSettings _settings = settings;
    private readonly IErrorService _errorService = errorService;

    public async Task<Result<FacebookUserProfileResponse>> VerifyAuthorizationCodeAsync(string code, string redirectUri)
    {
        try
        {
            if (string.IsNullOrEmpty(code))
                return _errorService.CreateFailure<FacebookUserProfileResponse>(ErrorCodes.FACEBOOK_AUTHORIZATION_CODE_REQUIRES);

            if (string.IsNullOrEmpty(redirectUri))
                return _errorService.CreateFailure<FacebookUserProfileResponse>(ErrorCodes.FACEBOOK_REDIRECT_URI_REQUIRED);

            // Exchange authorization code for access token
            var tokenUrl = $"{_settings.TokenUrl}?" +
                $"client_id={_settings.AppId}&" +
                $"redirect_uri={Uri.EscapeDataString(redirectUri)}&" +
                $"client_secret={_settings.AppSecret}&" +
                $"code={code}";

            var tokenResponse = await _httpClient.GetAsync(tokenUrl);

            if (!tokenResponse.IsSuccessStatusCode)
                return _errorService.CreateFailure<FacebookUserProfileResponse>(ErrorCodes.FACEBOOK_TOKEN_EXCHANGE_FAILED);

            var tokenContent = await tokenResponse.Content.ReadAsStringAsync();
            var tokenData = JsonSerializer.Deserialize<FacebookTokenResponse>(tokenContent);

            if (tokenData == null || string.IsNullOrEmpty(tokenData.AccessToken))
                return _errorService.CreateFailure<FacebookUserProfileResponse>(ErrorCodes.FACEBOOK_INVALID_TOKEN);

            // Get user profile from Facebook
            var userInfoUrl = $"{_settings.UserInfoUrl}?fields=id,email,first_name,last_name,name,picture&access_token={tokenData.AccessToken}";
            var userResponse = await _httpClient.GetAsync(userInfoUrl);

            if (!userResponse.IsSuccessStatusCode)
                return _errorService.CreateFailure<FacebookUserProfileResponse>(ErrorCodes.FACEBOOK_USER_PROFILE_FAILED);

            var userContent = await userResponse.Content.ReadAsStringAsync();
            var userData = JsonSerializer.Deserialize<FacebookUserProfileResponse>(userContent);

            if (userData == null)
                return _errorService.CreateFailure<FacebookUserProfileResponse>(ErrorCodes.FACEBOOK_INVALID_USER_DATA);

            return Result<FacebookUserProfileResponse>.Success(userData);
        }
        catch (Exception)
        {
            return _errorService.CreateFailure<FacebookUserProfileResponse>(ErrorCodes.EXTERNAL_SERVICE_ERROR);
        }
    }
}
