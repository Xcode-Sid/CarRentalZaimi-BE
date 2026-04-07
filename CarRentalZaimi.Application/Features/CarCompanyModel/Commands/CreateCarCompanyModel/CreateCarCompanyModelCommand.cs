using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.CarCompanyModel.Commands.CreateCarCompanyModel;

public class CreateCarCompanyModelCommand() : ICommand<CarCompanyModelDto>
{
    public string? CompanyNameId { get; set; }
    public string? Name { get; set; }
}
