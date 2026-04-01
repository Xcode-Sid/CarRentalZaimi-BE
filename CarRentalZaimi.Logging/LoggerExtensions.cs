using Microsoft.Extensions.Logging;

namespace CarRentalZaimi.Logging;

public static class LoggerExtensions
{
    private static readonly Action<ILogger, string, Exception?> LogDebug =
        LoggerMessage.Define<string>(LogLevel.Debug, new EventId(1, "Debug"), "{Message}");

    private static readonly Action<ILogger, string, Exception?> LogInfo =
        LoggerMessage.Define<string>(LogLevel.Information, new EventId(2, "Info"), "{Message}");

    private static readonly Action<ILogger, string, Exception?> LogWarn =
        LoggerMessage.Define<string>(LogLevel.Warning, new EventId(3, "Warn"), "{Message}");

    private static readonly Action<ILogger, string, Exception?> LogErr =
        LoggerMessage.Define<string>(LogLevel.Error, new EventId(4, "Error"), "{Message}");

    public static void Debug(this ILogger logger, string message)
        => LogDebug(logger, message, null);

    public static void Info(this ILogger logger, string message)
        => LogInfo(logger, message, null);

    public static void Warn(this ILogger logger, string message)
        => LogWarn(logger, message, null);

    public static void Error(this ILogger logger, Exception ex)
        => LogErr(logger, ex.Message, ex);

    public static void Error(this ILogger logger, string message, Exception ex)
        => LogErr(logger, message, ex);
}
