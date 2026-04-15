using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.CreateOccupiedCarDays.Commands.DeleteOccupiedCarDays;

public class DeleteOccupiedCarDaysCommand : ICommand<bool>
{
    public string? Id { get; set; }
}
