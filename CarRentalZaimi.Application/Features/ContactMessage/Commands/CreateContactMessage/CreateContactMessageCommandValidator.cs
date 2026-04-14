using FluentValidation;

namespace CarRentalZaimi.Application.Features.ContactMessage.Commands.CreateContactMessage;

internal class CreateContactMessageCommandValidator : AbstractValidator<CreateContactMessageCommand>
{
    public CreateContactMessageCommandValidator()
    {
        RuleFor(x => x.FullName)
           .NotEmpty().WithMessage("Full name is required")
           .MaximumLength(200).WithMessage("Name cannot exceed 50 characters");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email must be a valid email address");

        RuleFor(x => x.Message)
           .NotEmpty().WithMessage("Address is required");
    }
}
