using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.CarReview.Commands.DeleteCarReview;

public class DeleteCarReviewCommand : ICommand<bool>
{
    public string? Id { get; set; }
}
