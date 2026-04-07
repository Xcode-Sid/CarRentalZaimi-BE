using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.CarCategory.Command.CreateCarCategory;

public class CreateCarCategoryCommand() : ICommand<CarCategoryDto>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}
