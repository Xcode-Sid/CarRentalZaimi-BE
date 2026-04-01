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

    // Authentication Errors (3100-3199)
    public const string AUTH_INVALID_TOKEN = "ERR_3101";
    public const string AUTH_TOKEN_EXPIRED = "ERR_3102";
    public const string AUTH_REFRESH_TOKEN_INVALID = "ERR_3103";
    public const string AUTH_REFRESH_TOKEN_EXPIRED = "ERR_3104";
    public const string AUTH_PASSWORD_RESET_TOKEN_INVALID = "ERR_3105";
    public const string AUTH_PASSWORD_RESET_TOKEN_EXPIRED = "ERR_3106";
    public const string AUTH_EMAIL_NOT_CONFIRMED = "ERR_3107";
    public const string AUTH_EMAIL_ALREADY_CONFIRMED = "ERR_3108";
    public const string AUTH_ACCOUNT_LOCKED = "ERR_3109";
    public const string AUTH_ACCOUNT_SUSPENDED = "ERR_3110";
    public const string AUTH_INVALID_REFRESH_TOKEN = "ERR_3111";
    public const string AUTH_TOKEN_BLACKLISTED = "ERR_3112";


    // Google OAuth Errors (ERR_9005-9007)
    public const string GOOGLE_TOKEN_EXCHANGE_FAILED = "ERR_9005";
    public const string GOOGLE_INVALID_TOKEN = "ERR_9006";
    public const string GOOGLE_USER_PROFILE_FAILED = "ERR_9007";
    public const string GOOGLE_AUTHORIZATION_CODE_REQUIRES = "ERR_9008";
    public const string GOOGLE_REDIRECT_URI_REQUIRED = "ERR_9009";
    public const string GOOGLE_INVALID_USER_DATA = "ERR_9010";


    // Facebook OAuth Errors (ERR_9011-9016)
    public const string FACEBOOK_TOKEN_EXCHANGE_FAILED = "ERR_9011";
    public const string FACEBOOK_INVALID_TOKEN = "ERR_9012";
    public const string FACEBOOK_USER_PROFILE_FAILED = "ERR_9013";
    public const string FACEBOOK_AUTHORIZATION_CODE_REQUIRES = "ERR_9014";
    public const string FACEBOOK_REDIRECT_URI_REQUIRED = "ERR_9015";
    public const string FACEBOOK_INVALID_USER_DATA = "ERR_9016";


    // Microsoft OAuth Errors (ERR_9011-9016)
    public const string MICROSOFT_TOKEN_EXCHANGE_FAILED = "ERR_9017";
    public const string MICROSOFT_INVALID_TOKEN = "ERR_9018";
    public const string MICROSOFT_USER_PROFILE_FAILED = "ERR_9019";
    public const string MICROSOFT_AUTHORIZATION_CODE_REQUIRES = "ERR_9020";
    public const string MICROSOFT_REDIRECT_URI_REQUIRED = "ERR_9021";
    public const string MICROSOFT_INVALID_USER_DATA = "ERR_9022";
    public const string MICROSOFT_CODE_VERIFIER_REQUIRED = "ERR_9023";

    // Microsoft OAuth Errors (ERR_9024-9030)
    public const string YAHOO_TOKEN_EXCHANGE_FAILED = "ERR_9024";
    public const string YAHOO_INVALID_TOKEN = "ERR_9025";
    public const string YAHOO_USER_PROFILE_FAILED = "ERR_9026";
    public const string YAHOO_AUTHORIZATION_CODE_REQUIRES = "ERR_9027";
    public const string YAHOO_REDIRECT_URI_REQUIRED = "ERR_9028";
    public const string YAHOO_INVALID_USER_DATA = "ERR_9029";
    public const string YAHOO_CODE_VERIFIER_REQUIRED = "ERR_9030";


    public const string TOKEN_EXPIRED = "ERR_1040";
    public const string INVALID_TOKEN = "ERR_1041";
}
