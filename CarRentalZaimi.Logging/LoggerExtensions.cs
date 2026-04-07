using Microsoft.Extensions.Logging;

namespace CarRentalZaimi.Logging;

public static class LoggerExtensions
{
    public static void Debug(this ILogger logger, string message)
        => logger.Log(LogLevel.Debug, message);

    public static void Debug(this ILogger logger, string template, params object?[] args)
        => logger.Log(LogLevel.Debug, template, args);

    public static void Info(this ILogger logger, string message)
        => logger.Log(LogLevel.Information, message);

    public static void Info(this ILogger logger, string template, params object?[] args)
        => logger.Log(LogLevel.Information, template, args);

    public static void Warn(this ILogger logger, string message)
        => logger.Log(LogLevel.Warning, message);

    public static void Warn(this ILogger logger, string template, params object?[] args)
        => logger.Log(LogLevel.Warning, template, args);

    public static void Warn(this ILogger logger, Exception ex, string message)
        => logger.Log(LogLevel.Warning, ex, message);

    public static void Warn(this ILogger logger, Exception ex, string template, params object?[] args)
        => logger.Log(LogLevel.Warning, ex, template, args);

    public static void Error(this ILogger logger, string message)
        => logger.Log(LogLevel.Error, message);

    public static void Error(this ILogger logger, string template, params object?[] args)
        => logger.Log(LogLevel.Error, template, args);

    public static void Error(this ILogger logger, Exception ex)
        => logger.Log(LogLevel.Error, ex, ex.Message);

    public static void Error(this ILogger logger, Exception ex, string message)
        => logger.Log(LogLevel.Error, ex, message);

    public static void Error(this ILogger logger, Exception ex, string template, params object?[] args)
        => logger.Log(LogLevel.Error, ex, template, args);
}
