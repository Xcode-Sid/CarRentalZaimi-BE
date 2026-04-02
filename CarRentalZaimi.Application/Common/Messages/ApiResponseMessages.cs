namespace CarRentalZaimi.Application.Common.Messages;

public static class ApiResponseMessages
{
    public const string InvalidRequest = "Invalid request";
    public const string Unauthorized = "Unauthorized";
    public const string UnauthorizedDetail = "You are not authorized to perform this action";
    public const string UnauthorizedAccessResource = "You are not authorized to access this resource";
    public const string UserNotAuthenticated = "User not authenticated";
    public const string ResourceNotFound = "Resource not found";
    public const string RequestTimeout = "Request timeout";
    public const string RequestTimeoutDetail = "The request took too long to process";
    public const string InternalServerError = "Internal server error";
    public const string InternalServerErrorDetail = "An unexpected error occurred";
}
