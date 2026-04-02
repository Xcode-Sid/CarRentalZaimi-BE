using CarRentalZaimi.Application.DTOs.Base;
using CarRentalZaimi.Domain.Enums;

namespace CarRentalZaimi.Application.DTOs;

public class UserDeviceDto : BaseDto<Guid>
{
    public UserDto? User { get; set; }
    public DeviceType DeviceType { get; set; }
    public BrowserType Browser { get; set; }
    public string? BrowserVersion { get; set; }
    public OperatingSystemType OperatingSystem { get; set; }
    public string? OSVersion { get; set; }
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
    public DateTime LastLoginAt { get; set; }
    public bool IsActive { get; set; }
}
