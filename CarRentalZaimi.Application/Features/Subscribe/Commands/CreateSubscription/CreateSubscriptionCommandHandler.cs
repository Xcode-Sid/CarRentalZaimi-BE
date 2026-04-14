using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.Subscribe.Commands.CreateSubscription;

internal class CreateSubscriptionCommandHandler(ISubscribeService _subscribeService) : ICommandHandler<CreateSubscriptionCommand, SubscribeDto>
{
    public async Task<Result<SubscribeDto>> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
        => await _subscribeService.SubscribeAsync(request, cancellationToken);
}