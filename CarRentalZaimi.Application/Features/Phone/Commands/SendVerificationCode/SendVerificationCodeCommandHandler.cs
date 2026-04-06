using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Common.Errors;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace CarRentalZaimi.Application.Features.Phone.Commands.SendVerificationCode;

public class SendVerificationCodeCommandHandler(
    IPhoneService _phoneService,
    ILogger<SendVerificationCodeCommandHandler> _logger,
    IErrorService _errorService) : ICommandHandler<SendVerificationCodeCommand, bool>
{
    public async Task<Result<bool>> Handle(SendVerificationCodeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Send confirmation code requested for user {User}", request.UserId);

            var result = await _phoneService.SendVerificationCodeAsync(request.UserId, cancellationToken);

            if (result.IsSuccessful)
                _logger.LogInformation("Confirmation code sent successfully for user {User}", request.UserId);
            else
                _logger.LogWarning("Confirmation code sent failed user {User}: {Error}", request.UserId, result.ErrorResult);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Confirmation code sent error for user {User}", request.UserId);
            return _errorService.CreateFailure<bool>(ErrorCodes.EXTERNAL_SERVICE_ERROR);
        }
    }
}
