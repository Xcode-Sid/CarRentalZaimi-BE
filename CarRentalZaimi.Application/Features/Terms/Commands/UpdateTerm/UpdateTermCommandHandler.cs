using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.Terms.Commands.UpdateTerm;

internal class UpdateTermCommandHandler(ITermsService _termsService) : ICommandHandler<UpdateTermCommand, TermsDto>
{
    public async Task<Result<TermsDto>> Handle(UpdateTermCommand request, CancellationToken cancellationToken)
        => await _termsService.UpdateAsync(request, cancellationToken);
}

