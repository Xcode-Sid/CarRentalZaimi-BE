using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.Privacy.Commands.UpdatePrivacy;

internal class UpdatePrivacyCommandHandler(IPrivacyService _privacyService) : ICommandHandler<UpdatePrivacyCommand, PrivacyDto>
{
    public async Task<Result<PrivacyDto>> Handle(UpdatePrivacyCommand request, CancellationToken cancellationToken)
        => await _privacyService.UpdateAsync(request, cancellationToken);
}
