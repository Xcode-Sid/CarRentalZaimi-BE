using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;

namespace CarRentalZaimi.Application.Features.CarReview.Queries.GetAllCarReview;

public class GetAllCarReviewQuery : IQuery<IEnumerable<CarReviewDto>>
{
    public string? CarId { get; set; }
}
