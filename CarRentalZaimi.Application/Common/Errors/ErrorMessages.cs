using System.Globalization;
using System.Resources;

namespace CarRentalZaimi.Application.Common.Errors;

public static class ErrorMessages
{
    private const string DefaultLanguage = "sq";
    private static readonly string[] SupportedLanguages = ["sq", "en", "it"];
    private static readonly ResourceManager ResourceManager =
        new("CarRentalZaimi.Application.Common.Errors.ErrorMessages", typeof(ErrorMessages).Assembly);

    public static string GetMessage(string errorCode, string? language = null)
    {
        var culture = ResolveCulture(language);
        var message = ResourceManager.GetString(errorCode, culture)
            ?? ResourceManager.GetString(errorCode, new CultureInfo(DefaultLanguage));

        return message ?? $"Error message not found for code: {errorCode}";
    }

    public static IEnumerable<string> GetSupportedLanguages()
        => SupportedLanguages;

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
