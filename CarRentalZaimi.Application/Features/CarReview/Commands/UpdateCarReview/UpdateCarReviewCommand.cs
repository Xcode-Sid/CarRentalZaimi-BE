using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.CarReview.Commands.UpdateCarReview;

public record UpdateCarReviewCommand : ICommand<CarReviewDto>
{
    public string? Id { get; set; }
    public float Rating { get; set; }
    public string? Comment { get; set; }
}
