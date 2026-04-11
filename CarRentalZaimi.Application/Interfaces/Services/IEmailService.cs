using CarRentalZaimi.Application.Common;

namespace CarRentalZaimi.Application.Interfaces.Services;

public interface IEmailService
{
    Task<Result<bool>> SendForgotPasswordEmailAsync(string email, string firstName, string resetLink, CancellationToken cancellationToken = default);
    Task<Result<bool>> SendBookingRequestEmailToAdminAsync(string adminEmail, string userName, string carName, string bookingReference, 
        CancellationToken cancellationToken = default);
    Task<Result<bool>> SendBookingCancellationEmailToAdminAsync(string adminEmail, string userName, string carName, string bookingReference, string cancellationDate, 
        string cancellationReason, CancellationToken cancellationToken = default);
    Task<Result<bool>> SendBookingAcceptanceEmailToUserAsync( string userEmail, string userName, string carTitle, string carName, string startDate, string endDate,
        CancellationToken cancellationToken = default);
    Task<Result<bool>> SendBookingRefusalEmailToUserAsync(string userEmail, string userName, string carTitle, string carName, string reason, 
        CancellationToken cancellationToken = default);
}
