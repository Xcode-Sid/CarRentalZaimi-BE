using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.Cars.Commands.AddFeaturedCar;

public class AddFeaturedCarCommand : ICommand<bool>
{
    public string? CarId{ get; set; }
    public bool IsRecommended { get; set; }
}
