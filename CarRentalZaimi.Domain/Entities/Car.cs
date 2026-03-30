using CarRentalZaimi.Domain.Common;
using CarRentalZaimi.Domain.Enums;

namespace CarRentalZaimi.Domain.Entities;

public class Car : AuditedEntity<Guid>
{
    public int CategoryId { get; set; }
    public int Year { get; set; }
    public string? LicensePlate { get; set; } 
    public decimal PricePerDay { get; set; }
    public int Seats { get; set; }
    public int Doors { get; set; }  
    public int? Mileage { get; set; }
    public int? HorsePower { get; set; }
    public bool? AirConditioner { get; set; }
    public bool? ABS { get; set; }
    public bool? ElectricWindows { get; set; }
    public bool? HeatedSeats { get; set; }
    public bool? GPS { get; set; }
    public CarStatus? Status { get; set; }
    public virtual CarCompanyName? Name { get; set; }
    public virtual CarCompanyModel? Model { get; set; }
    public virtual CarExteriorColor? ExteriorColorType { get; set; }
    public virtual CarInteriorColor? InteriorColorType { get; set; }
    public  virtual CarTransmission? TransmissionType { get; set; }
    public virtual CarFuel? FuelType { get; set; }
    public ICollection<CarImages>? CarImages { get; set; }
}
