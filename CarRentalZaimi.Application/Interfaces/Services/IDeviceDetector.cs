using CarRentalZaimi.Domain.Entities;

namespace CarRentalZaimi.Application.Interfaces.Services;

public interface IDeviceDetector
{
    UserDevice ParseDevice(string? userAgent, string? ipAddress);
}
