using CarRentalZaimi.Application.Common.Errors;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using CarRentalZaimi.Logging;

namespace CarRentalZaimi.Application.Features.Authentication.Command.ChangePassword;


public class ChangePasswordCommandHandler(
    IAuthenticationService _authenticationService,
    ILogger<ChangePasswordCommandHandler> _logger,
    IErrorService _errorService,
    IHttpContextAccessor _httpContextAccessor) : ICommandHandler<ChangePasswordCommand, bool>
{
    public async Task<ApiResponse<bool>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Get user ID from JWT claims
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return _errorService.CreateFailure<bool>(ErrorCodes.UNAUTHORIZED);

            _logger.Info("Password change requested for user {UserId}", userId);

            var result = await _authenticationService.ChangePasswordAsync(userId, request.CurrentPassword, request.NewPassword);

            if (result.IsSuccess)
                _logger.Info("Password changed successfully for user {UserId}", userId);
            else
                _logger.Warn("Password change failed for user {UserId}: {Error}", userId, result.ErrorResult);

            return result;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Password change error");
            return _errorService.CreateFailure<bool>(ErrorCodes.EXTERNAL_SERVICE_ERROR);
        }
    }
}


