using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.AdditionalService.Commands.UpdateAdditionalService;

public record UpdateAdditionalServiceCommand : ICommand<AdditionalServiceDto>
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Icon { get; set; }
    public decimal PricePerDay { get; set; }
    public bool IsActive { get; set; }
}
