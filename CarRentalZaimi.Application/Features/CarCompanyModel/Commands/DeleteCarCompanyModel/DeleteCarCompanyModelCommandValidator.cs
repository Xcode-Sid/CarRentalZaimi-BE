using FluentValidation;

namespace CarRentalZaimi.Application.Features.CarCompanyModel.Commands.DeleteCarCompanyModel;

internal class DeleteCarCompanyModelCommandValidator : AbstractValidator<DeleteCarCompanyModelCommand>
{
    public DeleteCarCompanyModelCommandValidator()
    {
        RuleFor(x => x.Id)
         .NotEmpty().WithMessage("Id is required");
    }
}
