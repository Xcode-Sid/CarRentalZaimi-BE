using System.Globalization;
using Microsoft.Extensions.Primitives;

namespace CarRentalZaimi.API.Middleware;

public class RequestLanguageMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var requestedLanguage = GetRequestedLanguage(context);
        var languageCode = NormalizeLanguage(requestedLanguage);
        var culture = MapLanguageToCulture(languageCode);

        CultureInfo.CurrentCulture = culture;
        CultureInfo.CurrentUICulture = culture;
        context.Items["language"] = languageCode;
        context.Response.Headers["Content-Language"] = languageCode;

        await next(context);
    }

    private static string? GetRequestedLanguage(HttpContext context)
    {
        var queryLanguage = FirstNonEmpty(
            context.Request.Query["lang"],
            context.Request.Query["language"],
            context.Request.Query["locale"]);

        if (!string.IsNullOrWhiteSpace(queryLanguage))
            return queryLanguage;

        var headerLanguage = FirstNonEmpty(
            context.Request.Headers["X-Language"],
            context.Request.Headers["X-Lang"]);

        if (!string.IsNullOrWhiteSpace(headerLanguage))
            return headerLanguage;

        var acceptLanguage = context.Request.Headers.AcceptLanguage.ToString();
        return string.IsNullOrWhiteSpace(acceptLanguage)
            ? null
            : acceptLanguage.Split(',')[0].Split(';')[0].Trim();
    }

    private static string NormalizeLanguage(string? language)
    {
        if (string.IsNullOrWhiteSpace(language))
            return "sq";

        var normalized = language.Trim().ToLowerInvariant();
        if (normalized.Contains('-'))
            normalized = normalized.Split('-')[0];

        return normalized switch
        {
            "al" => "sq", // FE alias for Albanian
            "sq" => "sq",
            "en" => "en",
            "it" => "it",
            _ => "sq"
        };
    }

    private static CultureInfo MapLanguageToCulture(string languageCode)
    {
        return languageCode switch
        {
            "en" => new CultureInfo("en-US"),
            "it" => new CultureInfo("it-IT"),
            _ => new CultureInfo("sq-AL")
        };
    }

    private static string? FirstNonEmpty(params StringValues[] values)
    {
        foreach (var value in values)
        {
            var text = value.ToString();
            if (!string.IsNullOrWhiteSpace(text))
                return text;
        }

        return null;
    }
}
