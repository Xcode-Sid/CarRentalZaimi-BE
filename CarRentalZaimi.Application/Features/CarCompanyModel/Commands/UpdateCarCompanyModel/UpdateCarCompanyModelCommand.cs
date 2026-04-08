using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.CarCompanyModel.Commands.UpdateCarCompanyModel;

public record UpdateCarCompanyModelCommand() : ICommand<CarCompanyModelDto>
{
    public string? Id { get; set; }
    public string? Name { get; set; }
}

