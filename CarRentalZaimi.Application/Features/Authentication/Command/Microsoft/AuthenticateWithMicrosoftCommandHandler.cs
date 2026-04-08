using CarRentalZaimi.Application.Common.Errors;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CarRentalZaimi.Logging;

namespace CarRentalZaimi.Application.Features.Authentication.Command.Microsoft;

public class AuthenticateWithMicrosoftCommandHandler(
    IAuthenticationService _authenticationService,
    ILogger<AuthenticateWithMicrosoftCommandHandler> _logger,
    IErrorService _errorService,
    IMicrosoftOAuthService _microsoftOAuthService,
    IHttpContextAccessor _httpContextAccessor) : ICommandHandler<AuthenticateWithMicrosoftCommand, AuthenticationResponseDto>
{
    public async Task<ApiResponse<AuthenticationResponseDto>> Handle(AuthenticateWithMicrosoftCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var microsoftResult = await _microsoftOAuthService.VerifyAuthorizationCodeAsync(request.Code, request.CodeVerifier, request.RedirectUri);

            if (!microsoftResult.IsSuccess || microsoftResult.Data == null)
                return _errorService.CreateFailure<AuthenticationResponseDto>(
                    string.IsNullOrWhiteSpace(microsoftResult.ErrorResult)
                        ? "Failed to verify Microsoft authentication"
                        : microsoftResult.ErrorResult);

            var microsoftUser = microsoftResult.Data;

            string firstName = microsoftUser!.GivenName!;
            string lastName = microsoftUser.Surname!;

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                var nameParts = microsoftUser.UserPrincipalName!.Split(' ', 2);
                firstName = nameParts.Length > 0 ? nameParts[0] : microsoftUser.GivenName!;
                lastName = nameParts.Length > 1 ? nameParts[1] : microsoftUser.Surname!;
            }

            var userAgent = _httpContextAccessor.HttpContext?.Request.Headers["User-Agent"].ToString();

            var result = await _authenticationService.AuthenticateWithMicrosoftAsync(
                microsoftUser.Mail,
                firstName,
                lastName,
                microsoftUser.Id,
                userAgent);

            if (result.IsSuccess)
                _logger.Info("Authentication successful for email {Email}", microsoftUser.Mail);
            else
                _logger.Warn("Authentication failed for email {Email}: {Error}", microsoftUser.Mail, result.ErrorResult);

            return result;
        }
        catch (Exception ex)
        {
            return _errorService.CreateFailure<AuthenticationResponseDto>(ErrorCodes.EXTERNAL_SERVICE_ERROR);
        }
    }
}
