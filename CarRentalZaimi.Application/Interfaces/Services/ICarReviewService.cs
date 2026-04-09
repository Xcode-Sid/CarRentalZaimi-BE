using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.CarReview.Commands.CreateCarReview;
using CarRentalZaimi.Application.Features.CarReview.Commands.DeleteCarReview;
using CarRentalZaimi.Application.Features.CarReview.Commands.UpdateCarReview;
using CarRentalZaimi.Application.Features.CarReview.Queries.GetAllCarReview;
using CarRentalZaimi.Application.Features.CarReview.Queries.GetAllPagedCarReview;

namespace CarRentalZaimi.Application.Interfaces.Services;

public interface ICarReviewService
{
    Task<Result<CarReviewDto>> CreateCarReviewAsync(CreateCarReviewCommand request, CancellationToken cancellationToken = default);
    Task<Result<CarReviewDto>> UpdateCarReviewAsync(UpdateCarReviewCommand request, CancellationToken cancellationToken = default);
    Task<Result<bool>> DeleteCarReviewAsync(DeleteCarReviewCommand request, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<CarReviewDto>>> GetAllAsync(GetAllCarReviewQuery request, CancellationToken cancellationToken = default);
    Task<Result<PagedResponse<CarReviewDto>>> GetAllPagedAsync(GetAllPagedCarReviewQuery request, CancellationToken cancellationToken = default);
}
