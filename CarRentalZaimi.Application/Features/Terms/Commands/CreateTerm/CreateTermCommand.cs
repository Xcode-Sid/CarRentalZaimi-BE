using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.Terms.Commands.CreateTerm;

public class CreateTermCommand : ICommand<TermsDto>
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Icon { get; set; }
    public string? Color { get; set; }
    public bool IsActive { get; set; }
}
