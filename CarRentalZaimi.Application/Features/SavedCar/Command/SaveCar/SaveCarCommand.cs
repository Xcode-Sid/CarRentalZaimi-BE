using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.SavedCar.Command.SaveCar;

public class SaveCarCommand : ICommand<bool>
{
    public string? UserId { get; set; }
    public string? CarId { get; set; }
}
