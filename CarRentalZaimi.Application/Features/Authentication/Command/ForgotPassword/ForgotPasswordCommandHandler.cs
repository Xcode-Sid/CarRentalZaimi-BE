using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Common.Errors;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace CarRentalZaimi.Application.Features.Authentication.Command.ForgotPassword;


public class ForgotPasswordCommandHandler(
    IPasswordResetService _passwordResetService,
    ILogger<ForgotPasswordCommandHandler> _logger,
    IErrorService _errorService) : ICommandHandler<ForgotPasswordCommand, string>
{

    public async Task<Result<string>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Forgot password request for email {Email}", request.Email);

            var result = await _passwordResetService.GeneratePasswordResetTokenAsync(request.Email);

            if (result.IsSuccessful)
                _logger.LogInformation("Password reset token generated for email {Email}", request.Email);
            else
                _logger.LogWarning("Failed to generate password reset token for email {Email}: {Error}", request.Email, result.ErrorResult);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating password reset token for email {Email}", request.Email);
            return _errorService.CreateFailure<string>(ErrorCodes.EXTERNAL_SERVICE_ERROR);
        }
    }
}


