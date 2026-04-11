using FluentValidation;

namespace CarRentalZaimi.Application.Features.BookingRequest.Commands.CreateBookingRequest;

internal class CreateBookingRequestCommandValidator : AbstractValidator<CreateBookingRequestCommand>
{
    public CreateBookingRequestCommandValidator()
    {
        RuleFor(x => x.UserId)
           .NotEmpty().WithMessage("User Id is required");

        RuleFor(x => x.CarId)
           .NotEmpty().WithMessage("Car Id is required");

        RuleFor(x => x.StartDate)
           .NotEmpty().WithMessage("Start date is required");

        RuleFor(x => x.EndDate)
           .NotEmpty().WithMessage("End date Id is required");

        RuleFor(x => x.TotalPrice)
           .NotEmpty().WithMessage("Total price Id is required");

        RuleFor(x => x.PaymentMethod)
           .NotEmpty().WithMessage("Payment method Id is required");
    }
}
