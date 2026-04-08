using CarRentalZaimi.Application.Common.Errors;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CarRentalZaimi.Logging;

namespace CarRentalZaimi.Application.Features.Authentication.Command.Login;

public class LoginCommandHandler(
    IAuthenticationService _authenticationService,
    ILogger<LoginCommandHandler> _logger,
    IErrorService _errorService,
    IHttpContextAccessor _httpContextAccessor) : ICommandHandler<LoginCommand, AuthenticationResponseDto>
{
    public async Task<ApiResponse<AuthenticationResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.Info("Login attempt for {Login}", request.Login);

            var ipAddress = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
            var userAgent = _httpContextAccessor.HttpContext?.Request.Headers["User-Agent"].ToString();

            var result = await _authenticationService.LoginAsync(request.Login, request.Password, ipAddress, userAgent);

            if (result.IsSuccess)
                _logger.Info("Login successful for {Login}", request.Login);
            else
                _logger.Warn("Login failed for {Login}: {Error}", request.Login, result.ErrorResult);

            return result;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Login error for {Login}", request.Login);
            return _errorService.CreateFailure<AuthenticationResponseDto>(ErrorCodes.EXTERNAL_SERVICE_ERROR);
        }
    }
}
