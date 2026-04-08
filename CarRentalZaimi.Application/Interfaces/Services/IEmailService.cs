using CarRentalZaimi.Application.DTOs.ApiResponse;

namespace CarRentalZaimi.Application.Interfaces.Services;

public interface IEmailService
{
    Task<ApiResponse<bool>> SendForgotPasswordEmailAsync(string email, string firstName, string resetLink, CancellationToken cancellationToken = default);
}
