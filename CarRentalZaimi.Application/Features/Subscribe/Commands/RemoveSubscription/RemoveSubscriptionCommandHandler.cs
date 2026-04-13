using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.Subscribe.Commands.RemoveSubscription;

internal class RemoveSubscriptionCommandHandler(ISubscribeService _subscribeService) : ICommandHandler<RemoveSubscriptionCommand, bool>
{
    public async Task<Result<bool>> Handle(RemoveSubscriptionCommand request, CancellationToken cancellationToken)
        => await _subscribeService.UsubscribeAsync(request, cancellationToken);
}