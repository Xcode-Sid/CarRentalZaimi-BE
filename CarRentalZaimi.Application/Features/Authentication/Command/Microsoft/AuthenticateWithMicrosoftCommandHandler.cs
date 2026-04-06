using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Common.Errors;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CarRentalZaimi.Application.Features.Authentication.Command.Microsoft;

public class AuthenticateWithMicrosoftCommandHandler(
    IAuthenticationService _authenticationService,
    ILogger<AuthenticateWithMicrosoftCommandHandler> _logger,
    IErrorService _errorService,
    IMicrosoftOAuthService _microsoftOAuthService,
    IHttpContextAccessor _httpContextAccessor) : ICommandHandler<AuthenticateWithMicrosoftCommand, AuthenticationResponseDto>
{
    public async Task<Result<AuthenticationResponseDto>> Handle(AuthenticateWithMicrosoftCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var microsoftResult = await _microsoftOAuthService.VerifyAuthorizationCodeAsync(request.Code, request.CodeVerifier, request.RedirectUri);

            if (!microsoftResult.IsSuccessful || microsoftResult.Data == null)
                return _errorService.CreateFailure<AuthenticationResponseDto>(microsoftResult.ErrorResult ?? "Failed to verify Microsoft authentication");

            var googleUser = microsoftResult.Data;

            string firstName = googleUser.GivenName!;
            string lastName = googleUser.Surname!;

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                var nameParts = googleUser.UserPrincipalName!.Split(' ', 2);
                firstName = nameParts.Length > 0 ? nameParts[0] : googleUser.GivenName!;
                lastName = nameParts.Length > 1 ? nameParts[1] : googleUser.Surname!;
            }

            // Get device info from HTTP context
            var userAgent = _httpContextAccessor.HttpContext?.Request.Headers["User-Agent"].ToString();

            var result = await _authenticationService.AuthenticateWithMicrosoftAsync(
                googleUser.Mail,
                firstName,
                lastName,
                googleUser.Id,
                userAgent);

            if (result.IsSuccessful)
                _logger.LogInformation("Authentication successful for email {Email}", googleUser.Mail);
            else
                _logger.LogWarning("Authentication failed for email {Email}: {Error}", googleUser.Mail, result.ErrorResult);

            return result;
        }
        catch (Exception ex)
        {
            return _errorService.CreateFailure<AuthenticationResponseDto>(ErrorCodes.EXTERNAL_SERVICE_ERROR);
        }
    }
}