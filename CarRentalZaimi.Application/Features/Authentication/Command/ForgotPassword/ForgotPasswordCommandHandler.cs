using CarRentalZaimi.Application.Common.Errors;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;
using Microsoft.Extensions.Logging;
using CarRentalZaimi.Logging;

namespace CarRentalZaimi.Application.Features.Authentication.Command.ForgotPassword;


public class ForgotPasswordCommandHandler(
    IPasswordResetService _passwordResetService,
    ILogger<ForgotPasswordCommandHandler> _logger,
    IErrorService _errorService) : ICommandHandler<ForgotPasswordCommand, string>
{

    public async Task<ApiResponse<string>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.Info("Forgot password request for email {Email}", request.Email);

            var result = await _passwordResetService.GeneratePasswordResetTokenAsync(request.Email);

            if (result.IsSuccess)
                _logger.Info("Password reset token generated for email {Email}", request.Email);
            else
                _logger.Warn("Failed to generate password reset token for email {Email}: {Error}", request.Email, result.ErrorResult);

            return result;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Error generating password reset token for email {Email}", request.Email);
            return _errorService.CreateFailure<string>(ErrorCodes.EXTERNAL_SERVICE_ERROR);
        }
    }
}



