using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.Partner.Commands.CreatePartner;

public class CreatePartnerCommand : ICommand<PartnerDto>
{
    public string? Name { get; set; }
    public string? Initials { get; set; }
    public string? Color { get; set; }
    public bool IsActive { get; set; }
}
