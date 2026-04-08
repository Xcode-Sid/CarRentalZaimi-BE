using CarRentalZaimi.Application.DTOs.ApiResponse;

namespace CarRentalZaimi.Application.Interfaces.Services;

public interface IPhoneService
{
    Task<ApiResponse<bool>> SendVerificationCodeAsync(string userId, CancellationToken cancellationToken);
    Task<ApiResponse<bool>> ConfirmPhoneAsync(string userId, string token, CancellationToken cancellationToken);
}
