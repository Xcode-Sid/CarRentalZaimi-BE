using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Domain.Enums;

namespace CarRentalZaimi.Application.Features.Authentication.Command.Google;

public class AuthenticateWithGoogleCommand : ICommand<AuthenticationResponseDto>
{
    public required string Code { get; set; }
    public required string RedirectUri { get; set; }
    public DeviceType? DeviceType { get; set; }
    public string? UserAgent { get; set; }
    public string? OperatingSystem { get; set; }
    public string? Browser { get; set; }
    public string? LastIPAddress { get; set; }
}

