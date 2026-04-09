using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.CarReview.Commands.CreateCarReview;

public class CreateCarReviewCommand : ICommand<CarReviewDto>
{
    public string? UserId { get; set; }
    public string? CarId { get; set; }
    public float Rating { get; set; }
    public string? Comment { get; set; }
}
