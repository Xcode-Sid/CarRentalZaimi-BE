namespace CarRentalZaimi.Application.Common.Messages;

public static class NotificationMessages
{
    public static class Booking
    {
        public const string NewRequest = "has made a booking request for";
        public const string Confirmed = "Your booking for {0} {1} has been accepted.";
        public const string Rejected = "Your booking for {0} {1} has been refused.";
        public const string Cancelled = "has cancelled their booking for";
        public const string Completed = "Your booking for {0} {1} has been completed.";
        public const string AdminCompleted = "Booking for {0} {1} by {2} {3} has been completed.";
    }

    public static class Car
    {
        public const string Added = "New car added: {0}";
        public const string AddedAll = "Check out our new car: {0}!";
        public const string Updated = "Car updated: {0}";
        public const string Deleted = "Car deleted: {0}";
    }

    public static class Review
    {
        public const string Created = "New review for {0} by {1}: {2} stars.";
        public const string Updated = "Review for {0} updated by {1}.";
        public const string Deleted = "Review for {0} deleted.";
    }

    public static class Promotion
    {
        public const string Created = "New Promotion: {0}! Use code {1} for {2}% off.";
        public const string Updated = "Promotion updated: {0}.";
        public const string Deleted = "Promotion deleted: {0}.";
    }

    public static class User
    {
        public const string Registered = "New user registered: {0} {1} ({2})";
        public const string RegisteredViaProvider = "New user registered via {0}: {1} {2} ({3})";
        public const string ProfileUpdated = "User {0} {1} updated their profile.";
        public const string PhoneAdded = "User {0} {1} added a phone number.";
    }

    public static class Entity
    {
        public const string Added = "New {0} added: {1}";
        public const string Updated = "{0} updated: {1}";
        public const string Deleted = "{0} deleted: {1}";
    }

    public static class Subscribe
    {
        public const string Subscribed = "{0} has subscribed to get new cars latest promotions information.";
        public const string Unsubscribed = "{0} has unsubscribed from car promotions and updates.";
    }

    public static class Contact
    {
        public const string NewMessage = "{0} has sent a message with subject {1}.";
    }

    public static class SavedCar
    {
        public const string Action = "User {0} {1} {2} car: {3}";
    }

    public static class CompanyProfile
    {
        public const string Added = "Company profile added successfully.";
        public const string Updated = "Company profile updated successfully.";
    }
}
