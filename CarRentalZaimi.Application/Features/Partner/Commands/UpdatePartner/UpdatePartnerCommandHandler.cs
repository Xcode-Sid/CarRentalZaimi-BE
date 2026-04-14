using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.Partner.Commands.UpdatePartner;

internal class UpdatePartnerCommandHandler(IPartnerService _partnerService) : ICommandHandler<UpdatePartnerCommand, PartnerDto>
{
    public async Task<Result<PartnerDto>> Handle(UpdatePartnerCommand request, CancellationToken cancellationToken)
        => await _partnerService.UpdateAsync(request, cancellationToken);
}
