using CarRentalZaimi.Application.Common.Model;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Domain.Enums;

namespace CarRentalZaimi.Application.Features.Cars.Commands.UpdateCar;

public record UpdateCarCommand : ICommand<CarDto>
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? CarId { get; set; }
    public int Year { get; init; }
    public string? LicensePlate { get; init; }
    public decimal PricePerDay { get; init; }
    public int Seats { get; init; }
    public int Doors { get; init; }
    public int? Mileage { get; init; }
    public int? HorsePower { get; init; }
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
    public Guid? CategoryId { get; init; }
    public Guid? NameId { get; init; }
    public Guid? ModelId { get; init; }
    public Guid? ExteriorColorTypeId { get; init; }
    public Guid? InteriorColorTypeId { get; init; }
    public Guid? TransmissionTypeId { get; init; }
    public Guid? FuelTypeId { get; init; }
    public List<CarImagesCommand>? CarImages { get; set; }
}
