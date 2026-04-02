using CarRentalZaimi.Application.Common;

namespace CarRentalZaimi.Application.Interfaces.Services;

public interface IErrorService
{
    Result<T> CreateFailure<T>(string errorCode, string? language = null, params object[] parameters);

    Result<T> CreateFailure<T>(string errorCode, Exception? exception, string? language = null, params object[] parameters);

    /// <summary>
    /// Creates a failure result with localized error message
    /// </summary>
    /// <param name="errorCode">The error code</param>
    /// <param name="language">The language code (optional, uses current user's language if not provided)</param>
    /// <param name="parameters">Optional parameters for message formatting</param>
    /// <returns>Failure result with localized error message</returns>
    Result CreateFailure(string errorCode, string? language = null, params object[] parameters);

    Result CreateFailure(string errorCode, Exception? exception, string? language = null, params object[] parameters);

    /// <summary>
    /// Gets the localized error message for an error code
    /// </summary>
    /// <param name="errorCode">The error code</param>
    /// <param name="language">The language code (optional, uses current user's language if not provided)</param>
    /// <param name="parameters">Optional parameters for message formatting</param>
    /// <returns>Localized error message</returns>
    string GetErrorMessage(string errorCode, string? language = null, params object[] parameters);

    /// <summary>
    /// Gets the current user's language preference
    /// </summary>
    /// <returns>The user's language code</returns>
    string GetCurrentUserLanguage();
}
