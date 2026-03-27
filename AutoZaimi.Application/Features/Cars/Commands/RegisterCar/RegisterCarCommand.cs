using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.Cars.Commands.RegisterCar;

public class RegisterCarCommand : ICommand<CarDto>
{
    public string? Make { get; set; }
    public string? Model { get; set; }
    public int? Year { get; set; }
    public string? LicensePlate { get; set; }
    public string? Color { get; set; }
    public string? Category { get; set; }
    public decimal? PricePerDay { get; set; }
    public int? Mileage { get; set; }
}
