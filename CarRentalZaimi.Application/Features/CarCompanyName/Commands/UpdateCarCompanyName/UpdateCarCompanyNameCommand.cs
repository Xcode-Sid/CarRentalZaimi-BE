using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.CarCompanyName.Commands.UpdateCarCompanyName;

public record UpdateCarCompanyNameCommand() : ICommand<CarCompanyNameDto>
{
    public string? Id { get; set; }
    public string? Name { get; set; }
}

