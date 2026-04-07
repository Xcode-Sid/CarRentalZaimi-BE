using CarRentalZaimi.Application.Common.Errors;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CarRentalZaimi.Logging;

namespace CarRentalZaimi.Application.Features.Authentication.Command.Register;

public class RegisterCommandHandler(
    IAuthenticationService _authenticationService,
    ILogger<RegisterCommandHandler> _logger,
    IErrorService _errorService,
    IHttpContextAccessor _httpContextAccessor) : ICommandHandler<RegisterCommand, UserDto>
{
    public async Task<ApiResponse<UserDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.Info("Registration attempt for email {Email}", request.Email);

            var userAgent = _httpContextAccessor.HttpContext?.Request.Headers["User-Agent"].ToString();

            var result = await _authenticationService.RegisterAsync(
                request.Firstname!,
                request.Lastname!,
                request.DateOfBirth,
                request.Username!,
                request.Email!,
                request.Phone!,
                request.Password!,
                request.Name,
                request.Data,
                request.Role,
                userAgent);

            if (result.Success)
                _logger.Info("Registration successful for email {Email}", request.Email);
            else
                _logger.Warn("Registration failed for email {Email}: {Error}", request.Email, result.Errors.FirstOrDefault());

            return result;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Registration error for email {Email}", request.Email);
            return _errorService.CreateFailure<UserDto>(ErrorCodes.EXTERNAL_SERVICE_ERROR);
        }
    }
}
