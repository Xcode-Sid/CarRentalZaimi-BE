using CarRentalZaimi.Application.Common.Errors;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;
using Microsoft.Extensions.Logging;
using CarRentalZaimi.Logging;

namespace CarRentalZaimi.Application.Features.Phone.Commands.ConfirmPhone;

public class ConfirmPhoneCommandHandler(
    IPhoneService _phoneService,
    ILogger<ConfirmPhoneCommandHandler> _logger,
    IErrorService _errorService) : ICommandHandler<ConfirmPhoneCommand, bool>
{
    public async Task<ApiResponse<bool>> Handle(ConfirmPhoneCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.Info("Phone confirmation requested for user {UserId}", request.UserId);

            var result = await _phoneService.ConfirmPhoneAsync(request.UserId, request.Token, cancellationToken);

            if (result.IsSuccess)
                _logger.Info("Phone confirmed successfully for user {UserId}", request.UserId);
            else
                _logger.Warn("Phone confirmation failed for user {UserId}: {Error}", request.UserId, result.ErrorResult);

            return result;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Phone confirmation error for user {UserId}", request.UserId);
            return _errorService.CreateFailure<bool>(ErrorCodes.EXTERNAL_SERVICE_ERROR);
        }
    }
}

