using System.Text.RegularExpressions;
using CarRentalZaimi.Application.Interfaces.Services;
using CarRentalZaimi.Domain.Entities;
using CarRentalZaimi.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace CarRentalZaimi.Infrastructure.Services;

public partial class DeviceDetector(ILogger<DeviceDetector> _logger) : IDeviceDetector
{
    public UserDevice ParseDevice(string? userAgent, string? ipAddress)
    {
        _logger.LogDebug("Parsing device info from IP: {IpAddress}", ipAddress);
        userAgent ??= "Unknown";

        var device = new UserDevice
        {
            UserAgent = userAgent,
            IpAddress = ipAddress ?? "::1",
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

    private static string DetectBrowser(string ua)
    {
        if (ua.Contains("Edg/", StringComparison.OrdinalIgnoreCase)) return "Edge";
        if (ua.Contains("OPR/", StringComparison.OrdinalIgnoreCase) || ua.Contains("Opera", StringComparison.OrdinalIgnoreCase)) return "Opera";
        if (ua.Contains("Chrome/", StringComparison.OrdinalIgnoreCase) && !ua.Contains("Edg/", StringComparison.OrdinalIgnoreCase)) return "Chrome";
        if (ua.Contains("Safari/", StringComparison.OrdinalIgnoreCase) && !ua.Contains("Chrome/", StringComparison.OrdinalIgnoreCase)) return "Safari";
        if (ua.Contains("Firefox/", StringComparison.OrdinalIgnoreCase)) return "Firefox";
        return "Unknown";
    }

    private static string? DetectBrowserVersion(string ua)
    {
        var match = BrowserVersionRegex().Match(ua);
        return match.Success ? match.Groups[1].Value : null;
    }

    private static string DetectOS(string ua)
    {
        if (ua.Contains("Windows", StringComparison.OrdinalIgnoreCase)) return "Windows";
        if (ua.Contains("Mac OS X", StringComparison.OrdinalIgnoreCase) || ua.Contains("Macintosh", StringComparison.OrdinalIgnoreCase)) return "macOS";
        if (ua.Contains("Android", StringComparison.OrdinalIgnoreCase)) return "Android";
        if (ua.Contains("iPhone", StringComparison.OrdinalIgnoreCase) || ua.Contains("iPad", StringComparison.OrdinalIgnoreCase)) return "iOS";
        if (ua.Contains("Linux", StringComparison.OrdinalIgnoreCase)) return "Linux";
        return "Unknown";
    }

    private static string? DetectOSVersion(string ua)
    {
        var match = OSVersionRegex().Match(ua);
        return match.Success ? match.Groups[1].Value.Replace('_', '.') : null;
    }

    [GeneratedRegex(@"(iPad|Android(?!.*Mobile)|Tablet)", RegexOptions.IgnoreCase)]
    private static partial Regex TabletRegex();

    [GeneratedRegex(@"(iPhone|iPod|Android.*Mobile|webOS|BlackBerry|Opera Mini|IEMobile)", RegexOptions.IgnoreCase)]
    private static partial Regex MobileRegex();

    [GeneratedRegex(@"(?:Chrome|Firefox|Safari|Edg|OPR|Opera)[/\s](\d+[\.\d]*)", RegexOptions.IgnoreCase)]
    private static partial Regex BrowserVersionRegex();

    [GeneratedRegex(@"(?:Windows NT|Mac OS X|Android|CPU (?:iPhone )?OS)[/ ](\d+[._\d]*)", RegexOptions.IgnoreCase)]
    private static partial Regex OSVersionRegex();
}
