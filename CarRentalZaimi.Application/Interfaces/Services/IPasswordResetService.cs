using CarRentalZaimi.Application.Common;

namespace CarRentalZaimi.Application.Interfaces.Services;

public interface IPasswordResetService
{
    Task<Result<string>> GeneratePasswordResetTokenAsync(string email);
    Task<Result<bool>> ResetPasswordAsync(string token, string userId, string newPassword);
}
