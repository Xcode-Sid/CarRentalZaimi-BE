using System.Globalization;
using System.Resources;

namespace CarRentalZaimi.Application.Common.Messages;

public static class ApiResponseMessages
{
    private const string DefaultLanguage = "sq";
    private static readonly ResourceManager ResourceManager =
        new("CarRentalZaimi.Application.Common.Messages.ApiResponseMessages", typeof(ApiResponseMessages).Assembly);

    public static string InvalidRequest => GetMessage(nameof(InvalidRequest));
    public static string Unauthorized => GetMessage(nameof(Unauthorized));
    public static string UnauthorizedDetail => GetMessage(nameof(UnauthorizedDetail));
    public static string UnauthorizedAccessResource => GetMessage(nameof(UnauthorizedAccessResource));
    public static string UserNotAuthenticated => GetMessage(nameof(UserNotAuthenticated));
    public static string ResourceNotFound => GetMessage(nameof(ResourceNotFound));
    public static string RequestTimeout => GetMessage(nameof(RequestTimeout));
    public static string RequestTimeoutDetail => GetMessage(nameof(RequestTimeoutDetail));
    public static string InternalServerError => GetMessage(nameof(InternalServerError));
    public static string InternalServerErrorDetail => GetMessage(nameof(InternalServerErrorDetail));

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
