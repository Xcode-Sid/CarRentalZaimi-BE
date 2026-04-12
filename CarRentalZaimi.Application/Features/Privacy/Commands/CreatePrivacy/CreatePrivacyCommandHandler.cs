using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.Privacy.Commands.CreatePrivacy;

internal class CreatePrivacyCommandHandler(IPrivacyService _privacyService) : ICommandHandler<CreatePrivacyCommand, PrivacyDto>
{
    public async Task<Result<PrivacyDto>> Handle(CreatePrivacyCommand request, CancellationToken cancellationToken)
        => await _privacyService.CreateAsync(request, cancellationToken);
}
