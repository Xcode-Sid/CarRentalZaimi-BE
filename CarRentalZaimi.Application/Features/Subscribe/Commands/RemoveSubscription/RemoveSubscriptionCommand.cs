using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.Subscribe.Commands.RemoveSubscription;

public class RemoveSubscriptionCommand : ICommand<bool>
{
    public string? Token { get; set; }
}
