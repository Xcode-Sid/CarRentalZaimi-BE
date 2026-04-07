using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.CarCompanyModel.Commands.DeleteCarCompanyModel;

public class DeleteCarCompanyModelCommand() : ICommand<bool>
{
    public string? Id { get; set; }
}
