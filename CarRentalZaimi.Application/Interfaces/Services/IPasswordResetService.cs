using CarRentalZaimi.Application.DTOs.ApiResponse;

namespace CarRentalZaimi.Application.Interfaces.Services;

public interface IPasswordResetService
{
    Task<ApiResponse<string>> GeneratePasswordResetTokenAsync(string email);
    Task<ApiResponse<bool>> ResetPasswordAsync(string token, string email, string newPassword);
}
