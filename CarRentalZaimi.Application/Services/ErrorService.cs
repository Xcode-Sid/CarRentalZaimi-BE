using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Common.Errors;
using CarRentalZaimi.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CarRentalZaimi.Application.Services;

public class ErrorService(IHttpContextAccessor _httpContextAccessor, ILogger<ErrorService> _logger) : IErrorService
{
    public Result<T> CreateFailure<T>(string errorCode, string? language = null, params object[] parameters)
    {
        return CreateFailure<T>(errorCode, exception: null, language, parameters);
    }

    public Result<T> CreateFailure<T>(string errorCode, Exception? exception, string? language = null, params object[] parameters)
    {
        var userLanguage = language ?? GetCurrentUserLanguage();
        var message = GetErrorMessage(errorCode, userLanguage, parameters);

        if (exception != null)
            _logger.LogError(exception, "Error occurred: {ErrorCode} - {Message}", errorCode, message);
        else
            _logger.LogError("Error occurred: {ErrorCode} - {Message}", errorCode, message);

        return Result<T>.Error(message);
    }

    public Result CreateFailure(string errorCode, string? language = null, params object[] parameters)
    {
        return CreateFailure(errorCode, exception: null, language, parameters);
    }

    public Result CreateFailure(string errorCode, Exception? exception, string? language = null, params object[] parameters)
    {
        var userLanguage = language ?? GetCurrentUserLanguage();
        var message = GetErrorMessage(errorCode, userLanguage, parameters);

        if (exception != null)
            _logger.LogError(exception, "Error occurred: {ErrorCode} - {Message}", errorCode, message);
        else
            _logger.LogError("Error occurred: {ErrorCode} - {Message}", errorCode, message);

        return Result.Error(message);
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
                _logger.LogError(ex, "Failed to format error message for code {ErrorCode}", errorCode);
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
                // RouteValues not available in Application layer
                // var routeLanguage = httpContext.Request.RouteValues["language"]?.ToString();
                // if (!string.IsNullOrEmpty(routeLanguage))
                //     return routeLanguage!;

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
            _logger.LogError(ex, "Error getting current user language");
        }

        return "en";
    }
}
