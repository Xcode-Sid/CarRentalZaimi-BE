using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;

namespace CarRentalZaimi.Application.Features.CarReview.Queries.GetAllPagedCarReview;

public class GetAllPagedCarReviewQuery : IQuery<PagedResponse<CarReviewDto>>
{
    public int PageNr { get; set; } = 1;
    public int PageSize { get; set; } = 3;
}
