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
    Task<Result<bool>> SendNewSubscriptionEmailToAdminAsync(string adminEmail, string subscriberEmail,
        CancellationToken cancellationToken = default);
    Task<Result<bool>> SendUnsubscribeEmailToAdminAsync(string adminEmail, string subscriberEmail,
        CancellationToken cancellationToken = default);
    Task<Result<bool>> SendNewCarNotificationEmailAsync(  string subscriberEmail, string carTitle, string carModel, string carYear, string fuelType, string seats,
        string pricePerDay, string carUrl, CancellationToken cancellationToken = default);
    Task<Result<bool>> SendNewPromotionNotificationEmailAsync(string subscriberEmail, string title, string description, string code, string discountPercentage,
        string numberOfDays, string appliesTo, string fleetUrl, CancellationToken cancellationToken = default);
    Task<Result<bool>> SendContactMessageAdminNotificationAsync( string fullName, string email, string phone, string subject, string message, string adminEmail,
        CancellationToken cancellationToken = default);

}
