using System.Globalization;
using System.Resources;

namespace CarRentalZaimi.Application.Common.Errors;

public static class ExceptionMessages
{
    private const string DefaultLanguage = "sq";
    private static readonly ResourceManager ResourceManager =
        new("CarRentalZaimi.Application.Common.Errors.ExceptionMessages", typeof(ExceptionMessages).Assembly);

    // Configuration
    public static string JwtSecretKeyNotConfigured => GetMessage(nameof(JwtSecretKeyNotConfigured));
    public static string HangfireConnectionRequired => GetMessage(nameof(HangfireConnectionRequired));
    public static string ConnectionStringNotFound => GetMessage(nameof(ConnectionStringNotFound));
    public static string ConnectionStringNotFoundPostgres => GetMessage(nameof(ConnectionStringNotFoundPostgres));

    // Authentication / JWT
    public static string InvalidToken => GetMessage(nameof(InvalidToken));
    public static string UserIdNotFound => GetMessage(nameof(UserIdNotFound));

    private static string GetMessage(string key, string? language = null)
    {
        var culture = ResolveCulture(language);
        var message = ResourceManager.GetString(key, culture)
            ?? ResourceManager.GetString(key, new CultureInfo(DefaultLanguage));

        return message ?? key;
    }

    private static CultureInfo ResolveCulture(string? language)
    {
        if (string.IsNullOrWhiteSpace(language))
            return CultureInfo.CurrentUICulture.Name == "iv"
                ? new CultureInfo(DefaultLanguage)
                : CultureInfo.CurrentUICulture;

        try
        {
            return new CultureInfo(language);
        }
        catch (CultureNotFoundException)
        {
            return new CultureInfo(DefaultLanguage);
        }
    }
}
