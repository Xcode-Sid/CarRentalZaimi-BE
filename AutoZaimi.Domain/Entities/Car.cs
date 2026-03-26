using AutoZaimi.Domain.Common;
using AutoZaimi.Domain.Enums;

namespace AutoZaimi.Domain.Entities;

public class Car : AuditedEntity<Guid>
{
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
