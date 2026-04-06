using CarRentalZaimi.Domain.Common;
using CarRentalZaimi.Domain.Enums;

namespace CarRentalZaimi.Domain.Entities;

public class Car : AuditedEntity<Guid>
{
    public int Year { get; set; }
    public string? LicensePlate { get; set; } 
    public decimal PricePerDay { get; set; }
    public int Seats { get; set; }
    public int Doors { get; set; }  
    public int? Mileage { get; set; }
    public int? HorsePower { get; set; }
    public bool? ABS { get; set; }
    public bool? Bluetooth { get; set; }
    public bool? ParkingSensors { get; set; }
    public bool? CruiseControl { get; set; }
    public bool? ClimateControl { get; set; }
    public bool? LEDHeadlights { get; set; }
    public bool? AppleCarPlay { get; set; }
    public bool? AndroidAuto { get; set; }
    public bool? LaneDepartureAlert { get; set; }
    public bool? AdaptiveCruiseControl { get; set; }
    public bool? ToyotaSafetySense { get; set; }
    public bool? HeatedSeats { get; set; }
    public bool? PanoramicRoof { get; set; }
    public bool? ThirdRowSeats { get; set; }
    public bool? WirelessCharging { get; set; }
    public bool? Camera { get; set; }
    public bool? AirConditioner { get; set; }
    public bool? ElectricWindows { get; set; }
    public bool? GPS { get; set; }
    public CarStatus? Status { get; set; }
    public virtual CarCategory? Category { get; set; }
    public virtual CarCompanyName? Name { get; set; }
    public virtual CarCompanyModel? Model { get; set; }
    public virtual CarExteriorColor? ExteriorColorType { get; set; }
    public virtual CarInteriorColor? InteriorColorType { get; set; }
    public  virtual CarTransmission? TransmissionType { get; set; }
    public virtual CarFuel? FuelType { get; set; }
    public ICollection<CarImages>? CarImages { get; set; }
}
