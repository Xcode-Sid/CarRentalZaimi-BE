using FluentValidation;

namespace CarRentalZaimi.Application.Features.BookingRequest.Commands.RefuseBooking;

internal class RefuseBookingCommandValidator : AbstractValidator<RefuseBookingCommand>
{
    public RefuseBookingCommandValidator()
    {
        RuleFor(x => x.BookingId)
           .NotEmpty().WithMessage("Booking Id is required");

        RuleFor(x => x.RefusedReanson)
           .NotEmpty().WithMessage("Refused reanson Id is required");
    }
}