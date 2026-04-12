using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.Partner.Commands.DeletePartner;

public class DeletePartnerCommand : ICommand<bool>
{
    public string? Id { get; set; }
}
