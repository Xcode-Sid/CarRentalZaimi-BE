using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.AdditionalService.Commands.DeleteAdditionalService;

public class DeleteAdditionalServiceCommand : ICommand<bool>
{
    public string? Id { get; set; }
}
