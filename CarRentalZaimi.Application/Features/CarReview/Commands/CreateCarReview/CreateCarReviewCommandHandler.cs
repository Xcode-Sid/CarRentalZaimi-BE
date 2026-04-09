using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.CarReview.Commands.CreateCarReview;

internal class CreateCarReviewCommandHandler(ICarReviewService _carReviewService) : ICommandHandler<CreateCarReviewCommand, CarReviewDto>
{
    public async Task<Result<CarReviewDto>> Handle(CreateCarReviewCommand request, CancellationToken cancellationToken)
        => await _carReviewService.CreateCarReviewAsync(request, cancellationToken);
}
