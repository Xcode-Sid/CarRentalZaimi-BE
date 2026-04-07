using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Domain.Exceptions;
using CarRentalZaimi.Logging;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace CarRentalZaimi.API.Handlers;

public sealed class GlobalExceptionHandler(
    ILogger<GlobalExceptionHandler> _logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.Error(exception,
            "Unhandled {ExceptionType} on {Method} {Path} — TraceId: {TraceId}",
            exception.GetType().Name,
            httpContext.Request.Method,
            httpContext.Request.Path,
            httpContext.TraceIdentifier);

        var statusCode = MapStatusCode(exception);
        httpContext.Response.StatusCode = statusCode;
        httpContext.Response.ContentType = "application/json";

        var message = GetSafeMessage(exception, httpContext);
        var response = ApiResponse.FailureResponse(message);

        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
        return true;
    }

    private static int MapStatusCode(Exception exception) => exception switch
    {
        AppException app => (int)app.StatusCode,
        ArgumentNullException => StatusCodes.Status400BadRequest,
        ArgumentException => StatusCodes.Status400BadRequest,
        UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
        _ => StatusCodes.Status500InternalServerError
    };

    private static string GetSafeMessage(Exception exception, HttpContext context)
    {
        var env = context.RequestServices.GetRequiredService<IHostEnvironment>();

        if (env.IsDevelopment())
            return exception.Message;

        if (exception is AppException)
            return exception.Message;

        return "An unexpected error occurred.";
    }
}
