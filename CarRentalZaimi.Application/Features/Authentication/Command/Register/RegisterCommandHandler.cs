using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Common.Errors;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CarRentalZaimi.Application.Features.Authentication.Command.Register;


public class RegisterCommandHandler(
    IAuthenticationService _authenticationService,
    ILogger<RegisterCommandHandler> _logger,
    IErrorService _errorService,
    IHttpContextAccessor _httpContextAccessor) : ICommandHandler<RegisterCommand, UserDto>
{
    public async Task<Result<UserDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        try{
            _logger.LogInformation("Registration attempt for email {Email}", request.Email);

            // Get device info from HTTP context
            var userAgent = _httpContextAccessor.HttpContext?.Request.Headers["User-Agent"].ToString();

            var result = await _authenticationService.RegisterAsync(
                request.Firstname,
                request.Lastname,
                request.DateOfBirth,
                request.Username,
                request.Email,
                request.Phone,
                request.Password,
                request.Name,
                request.Data,
                request.Role,
                request.Location,
                userAgent);


            if (result.IsSuccessful)
                _logger.LogInformation("Registration successful for email {Email}", request.Email);
            else
                _logger.LogWarning("Registration failed for email {Email}: {Error}", request.Email, result.ErrorResult);

            return result;
        }
        catch (Exception ex) {
            _logger.LogError(ex, "Registration error for email {Email}", request.Email);
            return _errorService.CreateFailure<UserDto>(ErrorCodes.EXTERNAL_SERVICE_ERROR);
        }
    }
}
