using FluentValidation;

namespace CarRentalZaimi.Application.Features.Partner.Commands.DeletePartner;

internal class DeletePartnerCommandValidator : AbstractValidator<DeletePartnerCommand>
{
    public DeletePartnerCommandValidator()
    {
        RuleFor(x => x.Id)
          .NotEmpty().WithMessage("Id is required");

    }
}