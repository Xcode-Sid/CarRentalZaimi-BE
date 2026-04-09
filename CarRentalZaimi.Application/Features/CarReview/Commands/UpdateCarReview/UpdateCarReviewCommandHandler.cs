using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.CarReview.Commands.UpdateCarReview;

internal class UpdateCarReviewCommandHandler(ICarReviewService _carReviewService) : ICommandHandler<UpdateCarReviewCommand, CarReviewDto>
{
    public async Task<Result<CarReviewDto>> Handle(UpdateCarReviewCommand request, CancellationToken cancellationToken)
        => await _carReviewService.UpdateCarReviewAsync(request, cancellationToken);
}
