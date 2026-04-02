using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CarRentalZaimi.Application.Features.Authentication.Command.Facebook;

public class AuthenticateWithFacebookCommandHandler(
    IAuthenticationService _authenticationService,
    ILogger<AuthenticateWithFacebookCommandHandler> _logger,
    IErrorService _errorService,
    IFacebookOAuthService _facebookOAuthService,
    IHttpContextAccessor _httpContextAccessor) : ICommandHandler<AuthenticateWithFacebookCommand, AuthenticationResponseDto>
{
    public async Task<Result<AuthenticationResponseDto>> Handle(AuthenticateWithFacebookCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var facebookResult = await _facebookOAuthService.VerifyAuthorizationCodeAsync(request.Code, request.RedirectUri);

            if (!facebookResult.IsSuccessful || facebookResult.Data == null)
                return _errorService.CreateFailure<AuthenticationResponseDto>(facebookResult.ErrorMessage ?? "Failed to verify Facebook authentication");

            var facebookUser = facebookResult.Data;

            string firstName = facebookUser.FirstName!;
            string lastName = facebookUser.LastName!;

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                var nameParts = facebookUser.Name!.Split(' ', 2);
                firstName = nameParts.Length > 0 ? nameParts[0] : facebookUser.Name;
                lastName = nameParts.Length > 1 ? nameParts[1] : facebookUser.Name;
            }

            // Get device info from HTTP context
            var userAgent = _httpContextAccessor.HttpContext?.Request.Headers["User-Agent"].ToString();

            var result = await _authenticationService.AuthenticateWithFacebookAsync(
                facebookUser.Email,
                firstName,
                lastName,
                facebookUser.Picture?.Data?.Url,
                facebookUser.Id,
                userAgent);

            if (result.IsSuccessful)
                _logger.LogInformation("Authentication successful for email {Email}", facebookUser.Email);
            else
                _logger.LogWarning("Authentication failed for email {Email}: {Error}", facebookUser.Email, result.ErrorMessage);

            return result;
        }
        catch (Exception ex)
        {
            return _errorService.CreateFailure<AuthenticationResponseDto>(ex.Message);
        }
    }
}