using CarRentalZaimi.Domain.Common;
using CarRentalZaimi.Domain.Enums;

namespace CarRentalZaimi.Application.DTOs;

public class CarDto 
{
    public Guid Id { get; set; }
    public string? Make { get; set; }
    public string? Model { get; set; }
    public int? Year { get; set; }
    public string? LicensePlate { get; set; }
    public string? Color { get; set; }
    public CarCategory? Category { get; set; }
    public CarStatus? Status { get; set; }
    public decimal? PricePerDay { get; set; }
    public int? Mileage { get; set; }
}

