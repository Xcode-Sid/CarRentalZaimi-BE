using CarRentalZaimi.Application.Common;

namespace CarRentalZaimi.Application.Interfaces.Services;

public interface IEmailService
{
    Task<Result<bool>> SendForgotPasswordEmailAsync(string email, string firstName, string resetLink, CancellationToken cancellationToken = default);
    Task<Result<bool>> SendBookingRequestEmailToAdminAsync(string adminEmail, string userName, string carName, string bookingReference, CancellationToken cancellationToken = default);
}
