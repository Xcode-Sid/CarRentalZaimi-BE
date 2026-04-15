using FluentValidation;

namespace CarRentalZaimi.Application.Features.Ads.Commands.UpdateAds;

internal class UpdateAdsCommandValidator : AbstractValidator<UpdateAdsCommand>
{
    public UpdateAdsCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required");

        RuleFor(x => x.Title)
           .NotEmpty().WithMessage("Title is required")
           .MaximumLength(200).WithMessage("Title cannot exceed 200 characters");

        RuleFor(x => x.Position)
           .NotEmpty().WithMessage("Position is required");
    }
}