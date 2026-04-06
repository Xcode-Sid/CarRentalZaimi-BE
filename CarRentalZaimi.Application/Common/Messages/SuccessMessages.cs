namespace CarRentalZaimi.Application.Common.Messages;

public static class SuccessMessages
{
    public static class General
    {
        public const string OperationCompleted = "Operation completed successfully";
        public const string ResourceCreated = "Resource created successfully";
        public const string ResourceUpdated = "Resource updated successfully";
        public const string ResourceDeleted = "Resource deleted successfully";
    }

    public static class User
    {
        public const string UserProfileUpdated = "User profile updated successfully";
        public const string UserPhoneNumberUpdated = "User phone number successfully";
    }

    public static class Car
    {
        public const string CarCreated = "Car created successfully";
        public const string CarUpdated = "Car updated successfully";
        public const string CarDeleted = "Car deleted successfully";
    }
    public static class StatePrefix
    {
        public const string StatePrefixCreated = "State prefix created successfully";
        public const string StatePrefixUpdated = "State prefix updated successfully";
        public const string StatePrefixDeleted = "State prefix deleted successfully";
    }

    public static class CarFuel
    {
        public const string CarFuelCreated = "Car fuel created successfully";
        public const string CarFuelUpdated = "Car fuel updated successfully";
        public const string CarFuelDeleted = "Car fuel deleted successfully";
    }

    public static class CarTransmission
    {
        public const string CarTransmissionCreated = "Car transmission created successfully";
        public const string CarTransmissionUpdated = "Car transmission updated successfully";
        public const string CarTransmissionDeleted = "Car transmission deleted successfully";
    }

    public static class CarInteriorColor
    {
        public const string CarInteriorColorCreated = "Car interior color created successfully";
        public const string CarInteriorColorUpdated = "Car interior color updated successfully";
        public const string CarInteriorColorDeleted = "Car interior color deleted successfully";
    }
}
