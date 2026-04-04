using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Common.Errors;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Repositories;
using CarRentalZaimi.Application.Interfaces.Services;
using CarRentalZaimi.Application.Services;
using Microsoft.Extensions.Logging;

namespace CarRentalZaimi.Application.Features.Authentication.Command.ResetPassword;

public class ResetPasswordCommandHandler(
    IPasswordResetService _passwordResetService,
    ILogger<ResetPasswordCommandHandler> _logger,
    IErrorService _errorService) : ICommandHandler<ResetPasswordCommand, bool>
{

    public async Task<Result<bool>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Password reset request for email {Email}", request.Email);

            var result = await _passwordResetService.ResetPasswordAsync(request.Token, request.Email, request.NewPassword);

            if (result.IsSuccessful)
                _logger.LogInformation("Password reset successful for email {Email}", request.Email);
            else
                _logger.LogWarning("Password reset failed for email {Email}: {Error}", request.Email, result.ErrorMessage);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error resetting password for email {Email}", request.Email);
            return _errorService.CreateFailure<bool>(ErrorCodes.EXTERNAL_SERVICE_ERROR);
        }
    }
}