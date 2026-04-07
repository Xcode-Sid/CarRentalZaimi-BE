using CarRentalZaimi.Application.Common.Errors;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CarRentalZaimi.Logging;

namespace CarRentalZaimi.Application.Services;

public class ErrorService(IHttpContextAccessor _httpContextAccessor, ILogger<ErrorService> _logger) : IErrorService
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

    public string GetCurrentUserLanguage()
    {
        try
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext != null)
            {
                var queryLanguage = httpContext.Request.Query["lang"].FirstOrDefault();
                if (!string.IsNullOrEmpty(queryLanguage))
                    return queryLanguage;

                var acceptLanguage = httpContext.Request.Headers["Accept-Language"].FirstOrDefault();
                if (!string.IsNullOrEmpty(acceptLanguage))
                {
                    var languages = acceptLanguage.Split(',')
                        .Select(lang => lang.Split(';')[0].Trim())
                        .Select(lang => lang.Split('-')[0])
                        .ToList();

                    foreach (var lang in languages)
                    {
                        if (ErrorMessages.GetSupportedLanguages().Contains(lang))
                            return lang;
                    }
                }

                var user = httpContext.User;
                if (user?.Identity?.IsAuthenticated == true)
                {
                    var userLanguage = user.FindFirst("language")?.Value;
                    if (!string.IsNullOrEmpty(userLanguage))
                        return userLanguage;
                }
            }
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Error getting current user language");
        }

        return "en";
    }
}
