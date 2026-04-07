using FluentValidation;

namespace CarRentalZaimi.Application.Features.CarCategory.Command.DeleteCarCategory;

internal class DeleteCarCategoryCommandValidator : AbstractValidator<DeleteCarCategoryCommand>
{
    public DeleteCarCategoryCommandValidator()
    {
        RuleFor(x => x.Id)
         .NotEmpty().WithMessage("Id is required");
    }
}
