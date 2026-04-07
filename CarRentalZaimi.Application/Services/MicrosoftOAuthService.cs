using CarRentalZaimi.Application.Common.Errors;
using CarRentalZaimi.Application.Common.Microsoft;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.DTOs.Microsoft;
using CarRentalZaimi.Application.Interfaces.Services;
using System.Text.Json;

namespace CarRentalZaimi.Application.Services;

public class MicrosoftOAuthService(
    HttpClient httpClient,
    MicrosoftOAuthSettings settings,
    IErrorService errorService) : IMicrosoftOAuthService
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly MicrosoftOAuthSettings _settings = settings;
    private readonly IErrorService _errorService = errorService;

    public async Task<ApiResponse<MicrosoftUserProfileResponse>> VerifyAuthorizationCodeAsync(string code, string codeVerifier, string redirectUri)
    {
        try
        {
            if (string.IsNullOrEmpty(code))
                return _errorService.CreateFailure<MicrosoftUserProfileResponse>(ErrorCodes.MICROSOFT_AUTHORIZATION_CODE_REQUIRES);

            if (string.IsNullOrEmpty(codeVerifier))
                return _errorService.CreateFailure<MicrosoftUserProfileResponse>(ErrorCodes.MICROSOFT_CODE_VERIFIER_REQUIRED);

            if (string.IsNullOrEmpty(redirectUri))
                return _errorService.CreateFailure<MicrosoftUserProfileResponse>(ErrorCodes.MICROSOFT_REDIRECT_URI_REQUIRED);

            var tokenRequestContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("client_id", _settings.ClientId!),
                new KeyValuePair<string, string>("code", code),
                new KeyValuePair<string, string>("redirect_uri", redirectUri),
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                new KeyValuePair<string, string>("code_verifier", codeVerifier)
            });

            var tokenUrl = _settings.TokenUrl!.Replace("{tenantId}", _settings.TenantId);
            var tokenResponse = await _httpClient.PostAsync(tokenUrl, tokenRequestContent);

            if (!tokenResponse.IsSuccessStatusCode)
                return _errorService.CreateFailure<MicrosoftUserProfileResponse>(ErrorCodes.MICROSOFT_TOKEN_EXCHANGE_FAILED);

            var tokenContent = await tokenResponse.Content.ReadAsStringAsync();
            var tokenData = JsonSerializer.Deserialize<MicrosoftTokenResponse>(tokenContent);

            if (tokenData == null || string.IsNullOrEmpty(tokenData.AccessToken))
                return _errorService.CreateFailure<MicrosoftUserProfileResponse>(ErrorCodes.MICROSOFT_INVALID_TOKEN);

            var userRequest = new HttpRequestMessage(HttpMethod.Get, _settings.UserInfoUrl);
            userRequest.Headers.Add("Authorization", $"Bearer {tokenData.AccessToken}");

            var userResponse = await _httpClient.SendAsync(userRequest);

            if (!userResponse.IsSuccessStatusCode)
                return _errorService.CreateFailure<MicrosoftUserProfileResponse>(ErrorCodes.MICROSOFT_USER_PROFILE_FAILED);

            var userContent = await userResponse.Content.ReadAsStringAsync();
            var userData = JsonSerializer.Deserialize<MicrosoftUserProfileResponse>(userContent);

            if (userData == null)
                return _errorService.CreateFailure<MicrosoftUserProfileResponse>(ErrorCodes.MICROSOFT_INVALID_USER_DATA);

            return ApiResponse<MicrosoftUserProfileResponse>.SuccessResponse(userData);
        }
        catch (Exception)
        {
            return _errorService.CreateFailure<MicrosoftUserProfileResponse>(ErrorCodes.EXTERNAL_SERVICE_ERROR);
        }
    }
}
