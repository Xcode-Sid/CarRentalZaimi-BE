using FluentValidation;

namespace CarRentalZaimi.Application.Features.CarCompanyName.Commands.DeleteCarCompanyName;

internal class DeleteCarCompanyNameCommandValidator : AbstractValidator<DeleteCarCompanyNameCommand>
{
    public DeleteCarCompanyNameCommandValidator()
    {
        RuleFor(x => x.Id)
         .NotEmpty().WithMessage("Id is required");
    }
}
