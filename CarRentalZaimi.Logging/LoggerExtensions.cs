using Microsoft.Extensions.Logging;

namespace CarRentalZaimi.Logging;

public static class LoggerExtensions
{
    private static readonly Action<ILogger, Exception, Exception> LoggerErrorException;
    private static readonly Action<ILogger, string, Exception> LoggerInfo;

    static LoggerExtensions()
    {
        LoggerErrorException = LoggerMessage.Define<Exception>(LogLevel.Error, new EventId(9), "ERROR: {Exception}");
        LoggerInfo = LoggerMessage.Define<string>(LogLevel.Information, new EventId(3), "Information : {Message}");
    }


    public static void Error(this ILogger logger,
        Exception ex)
    {
        Console.WriteLine($"{ex.Message}{Environment.NewLine}{ex.StackTrace}");
        LoggerErrorException(logger, ex, null!);
    }

    public static void Info(this ILogger logger,
        string message,
        int eventId = 3)
    {
        Console.WriteLine(message);
        LoggerInfo(logger, message, null!);
    }
}

