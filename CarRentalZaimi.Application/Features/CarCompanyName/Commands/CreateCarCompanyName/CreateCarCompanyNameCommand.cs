using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.CarCompanyName.Commands.CreateCarCompanyName;

public class CreateCarCompanyNameCommand() : ICommand<CarCompanyNameDto>
{
    public string? Name { get; set; }
}
