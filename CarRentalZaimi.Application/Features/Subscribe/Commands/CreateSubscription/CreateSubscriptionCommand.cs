using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.Subscribe.Commands.CreateSubscription;

public class CreateSubscriptionCommand : ICommand<SubscribeDto>
{
    public string? Email { get; set; }
}
