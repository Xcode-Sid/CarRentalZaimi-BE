using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.Partner.Commands.CreatePartner;

internal class CreatePartnerCommandHandler(IPartnerService _partnerService) : ICommandHandler<CreatePartnerCommand, PartnerDto>
{
    public async Task<Result<PartnerDto>> Handle(CreatePartnerCommand request, CancellationToken cancellationToken)
        => await _partnerService.CreateAsync(request, cancellationToken);
}

