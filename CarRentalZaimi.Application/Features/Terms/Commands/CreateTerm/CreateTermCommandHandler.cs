using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.Terms.Commands.CreateTerm;

internal class CreateTermCommandHandler(ITermsService _termsService) : ICommandHandler<CreateTermCommand, TermsDto>
{
    public async Task<Result<TermsDto>> Handle(CreateTermCommand request, CancellationToken cancellationToken)
        => await _termsService.CreateAsync(request, cancellationToken);
}
