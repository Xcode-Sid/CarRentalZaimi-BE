using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.CarReview.Queries.GetAllCarReview;

internal class GetAllCarReviewQueryHandler(ICarReviewService _carReviewService) : IQueryHandler<GetAllCarReviewQuery, IEnumerable<CarReviewDto>>
{
    public async Task<Result<IEnumerable<CarReviewDto>>> Handle(GetAllCarReviewQuery request, CancellationToken cancellationToken)
        => await _carReviewService.GetAllAsync(request, cancellationToken);

}