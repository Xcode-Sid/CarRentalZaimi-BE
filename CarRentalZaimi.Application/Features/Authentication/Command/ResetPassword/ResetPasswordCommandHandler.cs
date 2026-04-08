using CarRentalZaimi.Application.Common.Errors;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;
using Microsoft.Extensions.Logging;
using CarRentalZaimi.Logging;

namespace CarRentalZaimi.Application.Features.Authentication.Command.ResetPassword;

public class ResetPasswordCommandHandler(
    IPasswordResetService _passwordResetService,
    ILogger<ResetPasswordCommandHandler> _logger,
    IErrorService _errorService) : ICommandHandler<ResetPasswordCommand, bool>
{

    public async Task<ApiResponse<bool>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.Info("Password reset request for email {Email}", request.Email);

            var result = await _passwordResetService.ResetPasswordAsync(request.Token, request.Email, request.NewPassword);

            if (result.IsSuccess)
                _logger.Info("Password reset successful for email {Email}", request.Email);
            else
                _logger.Warn("Password reset failed for email {Email}: {Error}", request.Email, result.ErrorResult);

            return result;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Error resetting password for email {Email}", request.Email);
            return _errorService.CreateFailure<bool>(ErrorCodes.EXTERNAL_SERVICE_ERROR);
        }
    }
}

