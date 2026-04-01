using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarRentalZaimi.Application.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(
    ILogger<LoggingBehavior<TRequest, TResponse>> _logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        _logger.LogInformation("Handling {RequestName} — {@Request}", requestName, request);

        var sw = Stopwatch.StartNew();

        try
        {
            var response = await next(cancellationToken);
            sw.Stop();

            _logger.LogInformation("Handled {RequestName} in {ElapsedMs}ms",
                requestName, sw.ElapsedMilliseconds);

            return response;
        }
        catch (Exception ex)
        {
            sw.Stop();

            _logger.LogError(ex,
                "Error handling {RequestName} after {ElapsedMs}ms — {ErrorMessage}",
                requestName, sw.ElapsedMilliseconds, ex.Message);

            throw;
        }
    }
}
