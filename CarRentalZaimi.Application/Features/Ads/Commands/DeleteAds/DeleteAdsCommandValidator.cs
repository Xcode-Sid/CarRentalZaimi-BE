using FluentValidation;

namespace CarRentalZaimi.Application.Features.Ads.Commands.DeleteAds;

internal class DeleteAdsCommandValidator : AbstractValidator<DeleteAdsCommand>
{
    public DeleteAdsCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required");
    }
}