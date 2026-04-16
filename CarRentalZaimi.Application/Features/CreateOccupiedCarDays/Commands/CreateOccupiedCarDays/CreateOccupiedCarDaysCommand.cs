using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.CreateOccupiedCarDays.Commands.CreateOccupiedCarDays;

public class CreateOccupiedCarDaysCommand : ICommand<OccupiedCarDaysDto>
{
    public string? CarId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? Type { get; set; }
}
