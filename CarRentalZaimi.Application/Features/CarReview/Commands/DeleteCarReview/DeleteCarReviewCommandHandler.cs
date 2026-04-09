using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.CarReview.Commands.DeleteCarReview;

internal class DeleteCarReviewCommandHandler(ICarReviewService _carReviewService) : ICommandHandler<DeleteCarReviewCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteCarReviewCommand request, CancellationToken cancellationToken)
        => await _carReviewService.DeleteCarReviewAsync(request, cancellationToken);
}
