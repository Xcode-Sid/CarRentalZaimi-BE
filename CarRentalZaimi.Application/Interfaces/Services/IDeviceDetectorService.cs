using CarRentalZaimi.Domain.Entities;

namespace CarRentalZaimi.Application.Interfaces.Services;

public interface IDeviceDetectorService
{
    UserDevice ParseDevice(string? userAgent, string? ipAddress);
}
