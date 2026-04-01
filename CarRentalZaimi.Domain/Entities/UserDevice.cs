using CarRentalZaimi.Domain.Common;
using CarRentalZaimi.Domain.Enums;

namespace CarRentalZaimi.Domain.Entities;

public class UserDevice : AuditedEntity<Guid>
{
    public virtual User? User { get; set; }
    public DeviceType DeviceType { get; set; }
    public string? Browser { get; set; }
    public string? BrowserVersion { get; set; }
    public string? OperatingSystem { get; set; }
    public string? OSVersion { get; set; }
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
    public DateTime LastLoginAt { get; set; }
    public bool IsActive { get; set; }
}
