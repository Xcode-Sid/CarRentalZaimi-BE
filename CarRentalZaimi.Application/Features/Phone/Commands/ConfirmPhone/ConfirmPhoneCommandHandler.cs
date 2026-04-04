using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Common.Errors;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace CarRentalZaimi.Application.Features.Phone.Commands.ConfirmPhone;

public class ConfirmPhoneCommandHandler(
    IPhoneService _phoneService,
    ILogger<ConfirmPhoneCommandHandler> _logger,
    IErrorService _errorService) : ICommandHandler<ConfirmPhoneCommand, bool>
{
    public async Task<Result<bool>> Handle(ConfirmPhoneCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Phone confirmation requested for user {UserId}", request.UserId);

            var result = await _phoneService.ConfirmPhoneAsync(request.UserId, request.Token, cancellationToken);

            if (result.IsSuccessful)
                _logger.LogInformation("Phone confirmed successfully for user {UserId}", request.UserId);
            else
                _logger.LogWarning("Phone confirmation failed for user {UserId}: {Error}", request.UserId, result.ErrorMessage);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Phone confirmation error for user {UserId}", request.UserId);
            return _errorService.CreateFailure<bool>(ErrorCodes.EXTERNAL_SERVICE_ERROR);
        }
    }
}