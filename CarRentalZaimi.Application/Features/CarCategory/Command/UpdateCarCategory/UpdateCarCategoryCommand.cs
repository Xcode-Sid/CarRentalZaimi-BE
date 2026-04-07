using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.CarCategory.Command.UpdateCarCategory;

public record UpdateCarCategoryCommand() : ICommand<CarCategoryDto>
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}
