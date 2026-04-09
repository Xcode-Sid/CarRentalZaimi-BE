using CarRentalZaimi.Application.DTOs.Base;
using CarRentalZaimi.Domain.Enums;

namespace CarRentalZaimi.Application.DTOs;

public class CarDto : BaseDto<Guid>
{
    public string? Title { get; set; }
    public string? Description { get; set; }
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
    public bool IsSaved { get; set; }
    public CarStatus? Status { get; set; }
    public CarCategoryDto? Category { get; set; }
    public CarCompanyNameDto? Name { get; set; }
    public CarCompanyModelDto? Model { get; set; }
    public CarExteriorColorDto? ExteriorColorType { get; set; }
    public CarInteriorColorDto? InteriorColorType { get; set; }
    public CarTransmissionDto? TransmissionType { get; set; }
    public CarFuelDto? FuelType { get; set; }
    public ICollection<CarImagesDto>? CarImages { get; set; }
}
