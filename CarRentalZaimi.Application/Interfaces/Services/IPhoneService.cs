using CarRentalZaimi.Application.Common;

namespace CarRentalZaimi.Application.Interfaces.Services;

public interface IPhoneService
{
    Task<Result<bool>> SendVerificationCodeAsync(string userId, CancellationToken cancellationToken);
    Task<Result<bool>> ConfirmPhoneAsync(string userId, string token, CancellationToken cancellationToken);
}
