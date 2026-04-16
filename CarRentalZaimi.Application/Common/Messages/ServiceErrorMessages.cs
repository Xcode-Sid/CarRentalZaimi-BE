namespace CarRentalZaimi.Application.Common.Messages;

public static class ServiceErrorMessages
{
    public static class General
    {
        public const string NotFound = "The requested resource was not found.";
        public const string OperationFailed = "The operation failed. Please try again.";
    }

    public static class User
    {
        public const string NotFound = "User not found.";
        public const string NotFoundById = "This user id does not exist.";
    }

    public static class Car
    {
        public const string NotFound = "Car not found.";
        public const string NotFoundById = "This car id does not exist.";
        public const string LicensePlateExists = "A car with this license plate already exists.";
        public const string CategoryNotFound = "Category not found.";
        public const string CompanyNameNotFound = "Car company name not found.";
        public const string ModelNotFound = "Car model not found.";
        public const string FuelNotFound = "Car fuel type not found.";
        public const string TransmissionNotFound = "Car transmission not found.";
        public const string ExteriorColorNotFound = "Car exterior color not found.";
        public const string InteriorColorNotFound = "Car interior color not found.";
        public const string UpdateFailed = "Failed to update car.";
        public const string DeleteFailed = "Failed to delete car.";
        public const string CreateFailed = "Failed to create car.";
    }

    public static class Booking
    {
        public const string NotFound = "Booking not found.";
        public const string CreateFailed = "Failed to create booking request.";
        public const string CancelFailed = "Failed to cancel booking.";
        public const string AcceptFailed = "Failed to accept booking.";
        public const string RefuseFailed = "Failed to refuse booking.";
        public const string CloseFailed = "Failed to close booking.";
        public const string AlreadyCancelledCannotAccept = "This booking has been cancelled and can't be accepted.";
        public const string AlreadyCancelledCannotRefuse = "This booking has been cancelled and can't be refused.";
        public const string AlreadyCancelledCannotClose = "This booking has been cancelled and can't be closed.";
        public const string AlreadyDoneCannotAccept = "This booking status is 'Done' and can't be accepted.";
        public const string AlreadyDoneCannotRefuse = "This booking status is 'Done' and can't be refused.";
        public const string AlreadyDoneCannotClose = "This booking is already closed.";
        public const string AlreadyRefusedCannotCancel = "This booking status is 'Done' and can't be cancelled.";
        public const string AlreadyRefusedCannotClose = "This booking has been refused and can't be closed.";
        public const string AlreadyAccepted = "This booking is already accepted.";
        public const string AlreadyRefused = "This booking has already been refused.";
        public const string MustBeAcceptedToClose = "Only accepted bookings can be closed.";
    }

    public static class Review
    {
        public const string NotFound = "Review not found.";
        public const string DuplicateReview = "User can't add more than one review per car.";
    }

    public static class Promotion
    {
        public const string NotFound = "This promotion id does not exist.";
        public const string CodeAlreadyExists = "A promotion with this code already exists.";
        public const string CannotTargetBoth = "A promotion can target either a specific car or a category, not both.";
        public const string CarNotFound = "The specified car was not found.";
        public const string CategoryNotFound = "The specified car category was not found.";
        public const string InvalidCarId = "Invalid car ID format.";
        public const string NoActivePromotion = "No active promotion found for this car.";
        public const string DuplicateCode = "This promotion already exists.";
        public const string NotFoundById = "This promotion id does not exists.";
    }

    public static class Term
    {
        public const string NotFound = "This term id does not exist.";
        public const string NotFoundUpdate = "This term id does not exists.";
    }

    public static class Privacy
    {
        public const string NotFound = "This privacy id does not exist.";
        public const string NotFoundUpdate = "This privacy id does not exists.";
    }

    public static class Partner
    {
        public const string NotFound = "This partner id does not exist.";
        public const string NotFoundUpdate = "This partner id does not exists.";
    }

    public static class AdditionalService
    {
        public const string NotFound = "This additional service id does not exist.";
        public const string NotFoundUpdate = "This additional service id does not exists.";
    }

    public static class CarCategory
    {
        public const string NotFound = "This car category id does not exist.";
        public const string NotFoundUpdate = "This car category id does not exists.";
    }

    public static class CarCompanyModel
    {
        public const string NotFound = "This car company model id does not exist.";
        public const string NotFoundUpdate = "This car company model id does not exists.";
    }

    public static class CarCompanyName
    {
        public const string NotFound = "This car company name id does not exist.";
        public const string NotFoundUpdate = "This car company name id does not exists.";
    }

    public static class CarExteriorColor
    {
        public const string NotFound = "This car exterior color id does not exist.";
        public const string NotFoundUpdate = "This car exterior color id does not exists.";
    }

    public static class CarInteriorColor
    {
        public const string NotFound = "This car interior color id does not exist.";
        public const string NotFoundUpdate = "This car interior color id does not exists.";
    }

    public static class CarFuel
    {
        public const string NotFound = "This car fuel id does not exist.";
        public const string NotFoundUpdate = "This car fuel id does not exists.";
    }

    public static class CarTransmission
    {
        public const string NotFound = "This car transmission id does not exist.";
        public const string NotFoundUpdate = "This car transmission id does not exists.";
    }

    public static class StatePrefix
    {
        public const string NotFound = "This state prefix id does not exist.";
        public const string NotFoundUpdate = "This state prefix id does not exists.";
    }

    public static class Subscribe
    {
        public const string AlreadySubscribed = "This email is already subscribed.";
        public const string NotSubscribed = "This email is not subscribed.";
    }

    public static class Notification
    {
        public const string NotFound = "Notification not found.";
        public const string NotFoundOrForbidden = "Notification not found or you do not have permission to access it.";
        public const string AdminFailed = "Failed to send admin notifications.";
    }

    public static class CompanyProfile
    {
        public const string NotFound = "Company profile not found.";
    }
}
