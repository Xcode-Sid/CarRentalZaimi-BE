using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.CarReview.Queries.GetAllPagedCarReview;

public class GetAllPagedCarReviewQueryHandler(ICarReviewService _carReviewService) : IQueryHandler<GetAllPagedCarReviewQuery, PagedResponse<CarReviewDto>>
{
    public async Task<Result<PagedResponse<CarReviewDto>>> Handle(GetAllPagedCarReviewQuery request, CancellationToken cancellationToken)
        => await _carReviewService.GetAllPagedAsync(request, cancellationToken);

}
