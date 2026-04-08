using CarRentalZaimi.Application.Common.Errors;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;
using Microsoft.Extensions.Logging;
using CarRentalZaimi.Logging;

namespace CarRentalZaimi.Application.Features.Authentication.Logout;

public class LogoutCommandHandler(
    IAuthenticationService _authenticationService,
    ILogger<LogoutCommandHandler> _logger,
    IErrorService _errorService) : ICommandHandler<LogoutCommand, bool>
{
    public async Task<ApiResponse<bool>> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.Info("Logout request for user {UserId}", request.UserId);

            if (string.IsNullOrWhiteSpace(request.UserId))
                return _errorService.CreateFailure<bool>(ErrorCodes.USER_NOT_FOUND);

            var result = await _authenticationService.LogoutAsync(request.UserId);

            if (result.IsSuccess)
                _logger.Info("User {UserId} logged out successfully", request.UserId);
            else
                _logger.Warn("Logout failed for user {UserId}: {Error}", request.UserId, result.ErrorResult);

            return result;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Error logging out user {UserId}", request.UserId);
            return _errorService.CreateFailure<bool>(ErrorCodes.EXTERNAL_SERVICE_ERROR);
        }
    }
}

