using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace CarRentalZaimi.API.Handlers;

public sealed class GlobalExceptionHandler(
    ILogger<GlobalExceptionHandler> _logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception,
            "Unhandled {ExceptionType} on {Method} {Path} — TraceId: {TraceId}",
            exception.GetType().Name,
            httpContext.Request.Method,
            httpContext.Request.Path,
            httpContext.TraceIdentifier);

        var statusCode = MapStatusCode(exception);
        httpContext.Response.StatusCode = statusCode;
        httpContext.Response.ContentType = "application/json";

        var result = Result.Error(
            exception,
            GetSafeMessage(exception, httpContext));

        await httpContext.Response.WriteAsJsonAsync(result, cancellationToken);
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

        return ResultMessages.UnexpectedError;
    }
}
