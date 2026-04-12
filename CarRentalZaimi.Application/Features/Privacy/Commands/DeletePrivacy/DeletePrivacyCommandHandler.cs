using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.Privacy.Commands.DeletePrivacy;

internal class DeletePrivacyCommandHandler(IPrivacyService _privacyService) : ICommandHandler<DeletePrivacyCommand, bool>
{
    public async Task<Result<bool>> Handle(DeletePrivacyCommand request, CancellationToken cancellationToken)
        => await _privacyService.DeleteAsync(request, cancellationToken);
}
