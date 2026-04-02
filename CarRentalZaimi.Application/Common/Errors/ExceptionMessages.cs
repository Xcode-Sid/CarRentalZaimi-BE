namespace CarRentalZaimi.Application.Common.Errors;

public static class ExceptionMessages
{
    // Configuration
    public const string JwtSecretKeyNotConfigured = "JWT SecretKey is not configured";
    public const string HangfireConnectionRequired = "Hangfire requires a database connection. Please configure either 'HangfireConnection' or 'DefaultConnection' in appsettings.json";
    public const string ConnectionStringNotFound = "ConnectionString or DefaultConnection not found in configuration";
    public const string ConnectionStringNotFoundPostgres = "ConnectionString or DefaultConnection not found in configuration. Please configure PostgreSQL connection string.";

    // Authentication / JWT
    public const string InvalidToken = "Invalid token";
    public const string UserIdNotFound = "User ID not found";
}

