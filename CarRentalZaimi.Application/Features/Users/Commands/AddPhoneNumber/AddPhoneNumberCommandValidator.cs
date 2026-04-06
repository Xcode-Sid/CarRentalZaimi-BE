using FluentValidation;

namespace CarRentalZaimi.Application.Features.Users.Commands.AddPhoneNumber
{
    public class AddPhoneNumberCommandValidator: AbstractValidator<AddPhoneNumberCommand>
    {
        public AddPhoneNumberCommandValidator()
        {

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User id is required");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithMessage("Phone number is required")
                .Matches(@"^\+?[1-9]\d{1,14}$")
                .WithMessage("Phone number must be a valid international format");
        }
    }
}
