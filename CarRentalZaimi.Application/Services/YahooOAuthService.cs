using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Common.Errors;
using CarRentalZaimi.Application.Common.Yahoo;
using CarRentalZaimi.Application.DTOs.Yahoo;
using CarRentalZaimi.Application.Interfaces.Services;
using System.Text.Json;

namespace CarRentalZaimi.Application.Services;


public class YahooOAuthService(
    HttpClient httpClient,
    YahooOAuthSettings settings,
    IErrorService errorService) : IYahooOAuthService
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly YahooOAuthSettings _settings = settings;
    private readonly IErrorService _errorService = errorService;

    public async Task<Result<YahooUserProfileResponse>> VerifyAuthorizationCodeAsync(string code, string codeVerifier, string redirectUri)
    {
        try
        {
            if (string.IsNullOrEmpty(code))
                return _errorService.CreateFailure<YahooUserProfileResponse>(ErrorCodes.YAHOO_AUTHORIZATION_CODE_REQUIRES);

            if (string.IsNullOrEmpty(codeVerifier))

                return _errorService.CreateFailure<YahooUserProfileResponse>(ErrorCodes.YAHOO_CODE_VERIFIER_REQUIRED);

            if (string.IsNullOrEmpty(redirectUri))
                return _errorService.CreateFailure<YahooUserProfileResponse>(ErrorCodes.YAHOO_REDIRECT_URI_REQUIRED);

            // Exchange authorization code for access token (PKCE flow)
            var tokenRequestContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("client_id", _settings.ClientId!),
                new KeyValuePair<string, string>("code", code),
                new KeyValuePair<string, string>("code_verifier", codeVerifier),
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                new KeyValuePair<string, string>("redirect_uri", redirectUri)
            });

            var tokenResponse = await _httpClient.PostAsync(_settings.TokenUrl, tokenRequestContent);

            if (!tokenResponse.IsSuccessStatusCode)
            {
                var errorContent = await tokenResponse.Content.ReadAsStringAsync();
                return _errorService.CreateFailure<YahooUserProfileResponse>(ErrorCodes.YAHOO_TOKEN_EXCHANGE_FAILED);
            }

            var tokenContent = await tokenResponse.Content.ReadAsStringAsync();
            var tokenData = JsonSerializer.Deserialize<YahooTokenResponse>(tokenContent);

            if (tokenData == null || string.IsNullOrEmpty(tokenData.AccessToken))
                return _errorService.CreateFailure<YahooUserProfileResponse>(ErrorCodes.YAHOO_INVALID_TOKEN);

            // Get user profile from Yahoo OpenID
            var userRequest = new HttpRequestMessage(HttpMethod.Get, _settings.UserInfoUrl);
            userRequest.Headers.Add("Authorization", $"Bearer {tokenData.AccessToken}");

            var userResponse = await _httpClient.SendAsync(userRequest);

            if (!userResponse.IsSuccessStatusCode)
                return _errorService.CreateFailure<YahooUserProfileResponse>(ErrorCodes.YAHOO_USER_PROFILE_FAILED);

            var userContent = await userResponse.Content.ReadAsStringAsync();
            var userData = JsonSerializer.Deserialize<YahooUserProfileResponse>(userContent);

            if (userData == null)
                return _errorService.CreateFailure<YahooUserProfileResponse>(ErrorCodes.YAHOO_INVALID_USER_DATA);

            return Result<YahooUserProfileResponse>.Success(userData);
        }
        catch (Exception)
        {
            return _errorService.CreateFailure<YahooUserProfileResponse>(ErrorCodes.EXTERNAL_SERVICE_ERROR);
        }
    }
}

