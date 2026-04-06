using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.CarCompanyName.Commands.DeleteCarCompanyName;

public class DeleteCarCompanyNameCommand() : ICommand<bool>
{
    public string? Id { get; set; }
}
