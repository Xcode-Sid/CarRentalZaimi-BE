using CarRentalZaimi.Application.Common.Model;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Domain.Enums;

namespace CarRentalZaimi.Application.Features.Cars.Commands.CreateCar
{
    public class CreateCarCommand : ICommand<CarDto>
    {
        public int Year { get; init; }
        public string? LicensePlate { get; init; }
        public decimal PricePerDay { get; init; }
        public int Seats { get; init; }
        public int Doors { get; init; }
        public int? Mileage { get; init; }
        public int? HorsePower { get; init; }
        public bool? AirConditioner { get; init; }
        public bool? ABS { get; init; }
        public bool? ElectricWindows { get; init; }
        public bool? HeatedSeats { get; init; }
        public bool? GPS { get; init; }
        public CarStatus? Status { get; init; }
        public Guid? CategoryId { get; init; }
        public Guid? NameId { get; init; }
        public Guid? ModelId { get; init; }
        public Guid? ExteriorColorTypeId { get; init; }
        public Guid? InteriorColorTypeId { get; init; }
        public Guid? TransmissionTypeId { get; init; }
        public Guid? FuelTypeId { get; init; }
        public List<CarImages>? CarImages { get; set; }
    }
}
