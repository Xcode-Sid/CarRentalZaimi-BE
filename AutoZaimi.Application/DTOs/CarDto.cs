using CarRentalZaimi.Application.DTOs.Base;
using CarRentalZaimi.Domain.Enums;

namespace CarRentalZaimi.Application.DTOs;

public class CarDto : BaseDto<Guid>
{
    public string? Make { get; set; }
    public string? Model { get; set; }
    public int? Year { get; set; }
    public string? LicensePlate { get; set; }
    public string? Color { get; set; }
    public string? Category { get; set; }
    public string? Status { get; set; }
    public decimal? PricePerDay { get; set; }
    public int? Mileage { get; set; }
}

