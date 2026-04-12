using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.Partner.Commands.UpdatePartner;

public record UpdatePartnerCommand : ICommand<PartnerDto>
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Initials { get; set; }
    public string? Color { get; set; }
    public bool IsActive { get; set; }
}
