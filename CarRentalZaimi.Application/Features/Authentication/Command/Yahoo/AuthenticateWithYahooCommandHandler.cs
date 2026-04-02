using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CarRentalZaimi.Application.Features.Authentication.Command.Yahoo;

internal class AuthenticateWithYahooCommandHandler(
IAuthenticationService _authenticationService,
ILogger<AuthenticateWithYahooCommandHandler> _logger,
IErrorService _errorService,
IYahooOAuthService _yahooOAuthService,
IHttpContextAccessor _httpContextAccessor) : ICommandHandler<AuthenticateWithYahooCommand, AuthenticationResponseDto>
{
    public async Task<Result<AuthenticationResponseDto>> Handle(AuthenticateWithYahooCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var yahooResult = await _yahooOAuthService.VerifyAuthorizationCodeAsync(request.Code, request.CodeVerifier, request.RedirectUri);

            if (!yahooResult.IsSuccessful || yahooResult.Data == null)
                return _errorService.CreateFailure<AuthenticationResponseDto>(yahooResult.ErrorMessage ?? "Failed to verify Yahoo authentication");

            var yahooUser = yahooResult.Data;

            string firstName = yahooUser.GivenName!;
            string lastName = yahooUser.FamilyName!;

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                var nameParts = yahooUser.Name!.Split(' ', 2);
                firstName = nameParts.Length > 0 ? nameParts[0] : yahooUser.GivenName!;
                lastName = nameParts.Length > 1 ? nameParts[1] : yahooUser.FamilyName!;
            }

            // Get IP address and device info from HTTP context
            var ipAddress = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
            var userAgent = _httpContextAccessor.HttpContext?.Request.Headers["User-Agent"].ToString();

            var result = await _authenticationService.AuthenticateWithYahooAsync(
                yahooUser.Email,
                firstName,
                lastName,
                yahooUser.Picture,
                yahooUser.Sub,
                userAgent);

            if (result.IsSuccessful)
                _logger.LogInformation(
                    "Authentication successful for {Email}", yahooUser.Email);
            else
                _logger.LogWarning(
                    "Authentication failed for {Email}: {Error}",
                    yahooUser.Email, result.ErrorMessage);

            return result;
        }
        catch (Exception ex)
        {
            return _errorService.CreateFailure<AuthenticationResponseDto>(ex.Message);
        }
    }
}
