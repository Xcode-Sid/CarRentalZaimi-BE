using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.StatePrefixes.Commands.CreateStatePrefix;

public class CreateStatePrefixCommand : ICommand<StatePrefixDto>
{
    public string? CountryName { get; set; }
    public string? PhonePrefix { get; set; }
    public string? Flag { get; set; }
    public string? PhoneRegex { get; set; }
}
