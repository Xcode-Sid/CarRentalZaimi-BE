using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Common.Errors;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace CarRentalZaimi.Application.Features.Authentication.Logout;

public class LogoutCommandHandler(
    IAuthenticationService _authenticationService,
    ILogger<LogoutCommandHandler> _logger,
    IErrorService _errorService) : ICommandHandler<LogoutCommand, bool>
{
    public async Task<Result<bool>> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Logout request for user {UserId}", request.UserId);

            if (string.IsNullOrWhiteSpace(request.UserId))
                return _errorService.CreateFailure<bool>(ErrorCodes.USER_NOT_FOUND);

            var result = await _authenticationService.LogoutAsync(request.UserId);

            if (result.IsSuccessful)
                _logger.LogInformation("User {UserId} logged out successfully", request.UserId);
            else
                _logger.LogWarning("Logout failed for user {UserId}: {Error}", request.UserId, result.ErrorMessage);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error logging out user {UserId}", request.UserId);
            return _errorService.CreateFailure<bool>(ErrorCodes.EXTERNAL_SERVICE_ERROR);
        }
    }
}
