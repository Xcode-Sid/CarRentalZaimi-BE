using FluentValidation;

namespace CarRentalZaimi.Application.Features.Partner.Commands.UpdatePartner;

internal class UpdatePartnerCommandValidator : AbstractValidator<UpdatePartnerCommand>
{
    public UpdatePartnerCommandValidator()
    {
        RuleFor(x => x.Id)
          .NotEmpty().WithMessage("Id is required");

        RuleFor(x => x.Name)
           .NotEmpty().WithMessage("Name is required")
           .MaximumLength(200).WithMessage("Name cannot exceed 200 characters");

        RuleFor(x => x.Color)
           .NotEmpty().WithMessage("Color is required");


        RuleFor(x => x.Initials)
           .NotEmpty().WithMessage("Initials is required");

    }
}