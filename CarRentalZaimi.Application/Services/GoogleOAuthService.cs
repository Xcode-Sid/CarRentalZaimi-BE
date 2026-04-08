using CarRentalZaimi.Application.Common.Errors;
using CarRentalZaimi.Application.Common.Google;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.DTOs.Google;
using CarRentalZaimi.Application.Interfaces.Services;
using System.Text.Json;

namespace CarRentalZaimi.Application.Services;

public class GoogleOAuthService(
    HttpClient httpClient,
    GoogleOAuthSettings settings,
    IErrorService errorService) : IGoogleOAuthService
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly GoogleOAuthSettings _settings = settings;
    private readonly IErrorService _errorService = errorService;

    public async Task<ApiResponse<GoogleUserProfileResponse>> VerifyAuthorizationCodeAsync(string code, string redirectUri)
    {
        try
        {
            if (string.IsNullOrEmpty(code))
                return _errorService.CreateFailure<GoogleUserProfileResponse>(ErrorCodes.GOOGLE_AUTHORIZATION_CODE_REQUIRES);

            if (string.IsNullOrEmpty(redirectUri))
                return _errorService.CreateFailure<GoogleUserProfileResponse>(ErrorCodes.GOOGLE_REDIRECT_URI_REQUIRED);

            var tokenRequestContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("client_id", _settings.ClientId!),
                new KeyValuePair<string, string>("client_secret", _settings.ClientSecret!),
                new KeyValuePair<string, string>("code", code),
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                new KeyValuePair<string, string>("redirect_uri", redirectUri)
            });

            var tokenResponse = await _httpClient.PostAsync(_settings.TokenUrl, tokenRequestContent);

            if (!tokenResponse.IsSuccessStatusCode)
                return _errorService.CreateFailure<GoogleUserProfileResponse>(ErrorCodes.GOOGLE_TOKEN_EXCHANGE_FAILED);

            var tokenContent = await tokenResponse.Content.ReadAsStringAsync();
            var tokenData = JsonSerializer.Deserialize<GoogleTokenResponse>(tokenContent);

            if (tokenData == null || string.IsNullOrEmpty(tokenData.AccessToken))
                return _errorService.CreateFailure<GoogleUserProfileResponse>(ErrorCodes.AUTH_INVALID_TOKEN);

            var userRequest = new HttpRequestMessage(HttpMethod.Get, _settings.UserInfoUrl);
            userRequest.Headers.Add("Authorization", $"Bearer {tokenData.AccessToken}");

            var userResponse = await _httpClient.SendAsync(userRequest);

            if (!userResponse.IsSuccessStatusCode)
                return _errorService.CreateFailure<GoogleUserProfileResponse>(ErrorCodes.GOOGLE_USER_PROFILE_FAILED);

            var userContent = await userResponse.Content.ReadAsStringAsync();
            var userData = JsonSerializer.Deserialize<GoogleUserProfileResponse>(userContent);

            if (userData == null)
                return _errorService.CreateFailure<GoogleUserProfileResponse>(ErrorCodes.GOOGLE_INVALID_USER_DATA);

            return ApiResponse<GoogleUserProfileResponse>.SuccessResponse(userData);
        }
        catch (Exception)
        {
            return _errorService.CreateFailure<GoogleUserProfileResponse>(ErrorCodes.EXTERNAL_SERVICE_ERROR);
        }
    }
}
