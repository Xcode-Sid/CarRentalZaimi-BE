using FluentValidation;

namespace CarRentalZaimi.Application.Features.Ads.Commands.CreateAds;

internal class CreateAdsCommandValidator : AbstractValidator<CreateAdsCommand>
{
    public CreateAdsCommandValidator()
    {
        RuleFor(x => x.Title)
           .NotEmpty().WithMessage("Title is required")
           .MaximumLength(200).WithMessage("Title cannot exceed 200 characters");

        RuleFor(x => x.Position)
           .NotEmpty().WithMessage("Position is required");
    }
}