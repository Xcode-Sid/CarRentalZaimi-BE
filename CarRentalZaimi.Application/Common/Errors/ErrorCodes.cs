namespace CarRentalZaimi.Application.Common.Errors;

public static class ErrorCodes
{
    // General Errors (1000-1999)
    public const string NOT_FOUND = "ERR_1001";
    public const string ALREADY_EXISTS = "ERR_1002";
    public const string INVALID_OPERATION = "ERR_1003";
    public const string UNAUTHORIZED = "ERR_1004";
    public const string FORBIDDEN = "ERR_1005";
    public const string VALIDATION_FAILED = "ERR_1006";
    public const string CONCURRENT_UPDATE = "ERR_1007";
    public const string EXTERNAL_SERVICE_ERROR = "ERR_1008";
    public const string INVALID_ARGUMENT = "ERR_1009";
    public const string DATABASE_ERROR = "ERR_1010";

    // User Errors (3000-3999)
    public const string USER_NOT_FOUND = "ERR_3001";
    public const string USER_ALREADY_EXISTS = "ERR_3002";
    public const string USER_INACTIVE = "ERR_3003";
    public const string USER_EMAIL_ALREADY_EXISTS = "ERR_3004";
    public const string USER_PHONE_ALREADY_EXISTS = "ERR_3005";
    public const string USER_INVALID_CREDENTIALS = "ERR_3006";
    public const string USER_PROFILE_NOT_FOUND = "ERR_3007";
    public const string USER_BANNED = "ERR_3008";
    public const string USER_SUSPENDED = "ERR_3009";
    public const string USER_STATUS_INVALID = "ERR_3010";
    public const string USER_STATUS_CHANGE_NOT_ALLOWED = "ERR_3011";
    public const string USER_ADMIN_PERMISSION_REQUIRED = "ERR_3012";
}
