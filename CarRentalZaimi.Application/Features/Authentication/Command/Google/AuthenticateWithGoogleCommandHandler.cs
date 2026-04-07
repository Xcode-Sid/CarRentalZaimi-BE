using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CarRentalZaimi.Logging;

namespace CarRentalZaimi.Application.Features.Authentication.Command.Google;

public class AuthenticateWithGoogleCommandHandler(
    IAuthenticationService _authenticationService,
    ILogger<AuthenticateWithGoogleCommandHandler> _logger,
    IErrorService _errorService,
    IGoogleOAuthService _googleOAuthService,
    IHttpContextAccessor _httpContextAccessor) : ICommandHandler<AuthenticateWithGoogleCommand, AuthenticationResponseDto>
{
    public async Task<ApiResponse<AuthenticationResponseDto>> Handle(AuthenticateWithGoogleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var googleResult = await _googleOAuthService.VerifyAuthorizationCodeAsync(request.Code, request.RedirectUri);

            if (!googleResult.Success || googleResult.Data == null)
                return _errorService.CreateFailure<AuthenticationResponseDto>(googleResult.Errors.FirstOrDefault() ?? "Failed to verify Google authentication");

            var googleUser = googleResult.Data;

            string firstName = googleUser.FamilyName!;
            string lastName = googleUser.GivenName!;

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                var nameParts = googleUser.Name!.Split(' ', 2);
                firstName = nameParts.Length > 0 ? nameParts[0] : googleUser.Name;
                lastName = nameParts.Length > 1 ? nameParts[1] : googleUser.Name;
            }

            var userAgent = _httpContextAccessor.HttpContext?.Request.Headers["User-Agent"].ToString();

            var result = await _authenticationService.AuthenticateWithGoogleAsync(
                googleUser.Email,
                firstName,
                lastName,
                googleUser.Picture,
                googleUser.Id,
                userAgent);

            if (result.Success)
                _logger.Info("Authentication successful for email {Email}", googleUser.Email);
            else
                _logger.Warn("Authentication failed for email {Email}: {Error}", googleUser.Email, result.Errors.FirstOrDefault());

            return result;
        }
        catch (Exception ex)
        {
            return _errorService.CreateFailure<AuthenticationResponseDto>(ex.Message);
        }
    }
}
