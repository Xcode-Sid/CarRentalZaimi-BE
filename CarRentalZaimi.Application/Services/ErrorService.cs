using CarRentalZaimi.Application.Common.Errors;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Interfaces.Services;
using Microsoft.Extensions.Logging;
using CarRentalZaimi.Logging;
using System.Globalization;

namespace CarRentalZaimi.Application.Services;

public class ErrorService(ILogger<ErrorService> _logger) : IErrorService
{
    public ApiResponse<T> CreateFailure<T>(string errorCode, string? language = null, params object[] parameters)
        => CreateFailure<T>(errorCode, exception: null, language, parameters);

    public ApiResponse<T> CreateFailure<T>(string errorCode, Exception? exception, string? language = null, params object[] parameters)
    {
        var userLanguage = language ?? GetCurrentUserLanguage();
        var message = GetErrorMessage(errorCode, userLanguage, parameters);

        if (exception != null)
            _logger.Error(exception, "Error occurred: {ErrorCode} - {Message}", errorCode, message);
        else
            _logger.Error("Error occurred: {ErrorCode} - {Message}", errorCode, message);

        return ApiResponse<T>.FailureResponse(message);
    }

    public ApiResponse CreateFailure(string errorCode, string? language = null, params object[] parameters)
        => CreateFailure(errorCode, exception: null, language, parameters);

    public ApiResponse CreateFailure(string errorCode, Exception? exception, string? language = null, params object[] parameters)
    {
        var userLanguage = language ?? GetCurrentUserLanguage();
        var message = GetErrorMessage(errorCode, userLanguage, parameters);

        if (exception != null)
            _logger.Error(exception, "Error occurred: {ErrorCode} - {Message}", errorCode, message);
        else
            _logger.Error("Error occurred: {ErrorCode} - {Message}", errorCode, message);

        return ApiResponse.FailureResponse(message);
    }

    public string GetErrorMessage(string errorCode, string? language = null, params object[] parameters)
    {
        var userLanguage = language ?? GetCurrentUserLanguage();
        var message = ErrorMessages.GetMessage(errorCode, userLanguage);

        if (parameters.Length > 0)
        {
            try
            {
                message = string.Format(message, parameters);
            }
            catch (FormatException ex)
            {
                _logger.Error(ex, "Failed to format error message for code {ErrorCode}", errorCode);
            }
        }

        return message;
    }

    private static string GetCurrentUserLanguage()
    {
        var uiCulture = CultureInfo.CurrentUICulture;
        if (uiCulture.Name == "iv")
            return "sq";

        var language = uiCulture.TwoLetterISOLanguageName.ToLowerInvariant();
        return ErrorMessages.GetSupportedLanguages().Contains(language)
            ? language
            : "sq";
    }
}
