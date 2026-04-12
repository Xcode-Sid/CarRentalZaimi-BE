using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.Partner.Commands.DeletePartner;

internal class DeletePartnerCommandHandler(IPartnerService _partnerService) : ICommandHandler<DeletePartnerCommand, bool>
{
    public async Task<Result<bool>> Handle(DeletePartnerCommand request, CancellationToken cancellationToken)
        => await _partnerService.DeleteAsync(request, cancellationToken);
}
