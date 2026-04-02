

using CarRentalZaimi.Application.Interfaces.Services;
using CarRentalZaimi.Domain.Entities;
using CarRentalZaimi.Domain.Enums;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace CarRentalZaimi.Application.Services;

public partial class DeviceDetectorService(
    ILogger<DeviceDetectorService> _logger,
    IUserContext userContext) : IDeviceDetectorService
{
    public UserDevice ParseDevice(string? userAgent, string? ipAddress)
    {
        var resolvedIp = ipAddress ?? userContext.IpAddress;
        _logger.LogDebug("Parsing device info from IP: {IpAddress}", resolvedIp);
        userAgent ??= "Unknown";

        var device = new UserDevice
        {
            UserAgent = userAgent,
            IpAddress = resolvedIp,
            DeviceType = DetectDeviceType(userAgent),
            Browser = DetectBrowser(userAgent),
            BrowserVersion = DetectBrowserVersion(userAgent),
            OperatingSystem = DetectOS(userAgent),
            OSVersion = DetectOSVersion(userAgent),
            LastLoginAt = DateTime.UtcNow,
            IsActive = true
        };

        _logger.LogInformation("Device parsed — {DeviceType} / {Browser} / {OS} from {IpAddress}",
            device.DeviceType, device.Browser, device.OperatingSystem, device.IpAddress);

        return device;
    }

    private static DeviceType DetectDeviceType(string ua)
    {
        if (TabletRegex().IsMatch(ua))
            return DeviceType.Tablet;
        if (MobileRegex().IsMatch(ua))
            return DeviceType.Mobile;
        return DeviceType.Desktop;
    }

    private static BrowserType DetectBrowser(string ua)
    {
        if (ua.Contains("Edg/", StringComparison.OrdinalIgnoreCase)) return BrowserType.Edge;
        if (ua.Contains("OPR/", StringComparison.OrdinalIgnoreCase) || ua.Contains("Opera", StringComparison.OrdinalIgnoreCase)) return BrowserType.Opera;
        if (ua.Contains("Chrome/", StringComparison.OrdinalIgnoreCase) && !ua.Contains("Edg/", StringComparison.OrdinalIgnoreCase)) return BrowserType.Chrome;
        if (ua.Contains("Safari/", StringComparison.OrdinalIgnoreCase) && !ua.Contains("Chrome/", StringComparison.OrdinalIgnoreCase)) return BrowserType.Safari;
        if (ua.Contains("Firefox/", StringComparison.OrdinalIgnoreCase)) return BrowserType.Firefox;
        return BrowserType.Unknown;
    }

    private static string? DetectBrowserVersion(string ua)
    {
        return DetectBrowser(ua) switch
        {
            BrowserType.Edge => MatchVersion(EdgeBrowserVersionRegex(), ua),
            BrowserType.Opera => MatchVersion(OperaBrowserVersionRegex(), ua),
            BrowserType.Chrome => MatchVersion(ChromeBrowserVersionRegex(), ua),
            BrowserType.Safari => MatchVersion(SafariBrowserVersionRegex(), ua),
            BrowserType.Firefox => MatchVersion(FirefoxBrowserVersionRegex(), ua),
            _ => null
        };
    }

    private static OperatingSystemType DetectOS(string ua)
    {
        if (ua.Contains("Windows", StringComparison.OrdinalIgnoreCase)) return OperatingSystemType.Windows;
        if (ua.Contains("Mac OS X", StringComparison.OrdinalIgnoreCase) || ua.Contains("Macintosh", StringComparison.OrdinalIgnoreCase)) return OperatingSystemType.MacOS;
        if (ua.Contains("Android", StringComparison.OrdinalIgnoreCase)) return OperatingSystemType.Android;
        if (ua.Contains("iPhone", StringComparison.OrdinalIgnoreCase) || ua.Contains("iPad", StringComparison.OrdinalIgnoreCase)) return OperatingSystemType.iOS;
        if (ua.Contains("Linux", StringComparison.OrdinalIgnoreCase)) return OperatingSystemType.Linux;
        return OperatingSystemType.Unknown;
    }

    private static string? DetectOSVersion(string ua)
    {
        var match = OSVersionRegex().Match(ua);
        return match.Success ? NormalizeVersion(match.Groups[1].Value) : null;
    }

    private static string? MatchVersion(Regex regex, string ua)
    {
        var m = regex.Match(ua);
        return m.Success ? NormalizeVersion(m.Groups[1].Value) : null;
    }

    private static string NormalizeVersion(string raw)
        => raw.Replace('_', '.').Trim();


    [GeneratedRegex(@"(iPad|Android(?!.*Mobile)|Tablet)", RegexOptions.IgnoreCase)]
    private static partial Regex TabletRegex();

    [GeneratedRegex(@"(iPhone|iPod|Android.*Mobile|webOS|BlackBerry|Opera Mini|IEMobile)", RegexOptions.IgnoreCase)]
    private static partial Regex MobileRegex();

    [GeneratedRegex(@"Edg/([\d.]+)", RegexOptions.IgnoreCase)]
    private static partial Regex EdgeBrowserVersionRegex();

    [GeneratedRegex(@"(?:OPR|Opera)/([\d.]+)", RegexOptions.IgnoreCase)]
    private static partial Regex OperaBrowserVersionRegex();

    [GeneratedRegex(@"Chrome/([\d.]+)", RegexOptions.IgnoreCase)]
    private static partial Regex ChromeBrowserVersionRegex();

    [GeneratedRegex(@"Version/([\d.]+)", RegexOptions.IgnoreCase)]
    private static partial Regex SafariBrowserVersionRegex();

    [GeneratedRegex(@"Firefox/([\d.]+)", RegexOptions.IgnoreCase)]
    private static partial Regex FirefoxBrowserVersionRegex();

    [GeneratedRegex(@"(?:Windows NT|Mac OS X|Android|CPU (?:iPhone )?OS)[/ ](\d+[._\d]*)", RegexOptions.IgnoreCase)]
    private static partial Regex OSVersionRegex();
}
