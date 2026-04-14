using FluentValidation;

namespace CarRentalZaimi.Application.Features.BookingRequest.Commands.CancelBooking;

internal class CancelBookingCommandValidator : AbstractValidator<CancelBookingCommand>
{
    public CancelBookingCommandValidator()
    {
        RuleFor(x => x.BookingId)
           .NotEmpty().WithMessage("Booking Id is required");
    }
}
