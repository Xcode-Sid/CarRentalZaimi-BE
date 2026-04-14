using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.Terms.Commands.DeleteTerm;

internal class DeleteTermCommandHandler(ITermsService _termsService) : ICommandHandler<DeleteTermCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteTermCommand request, CancellationToken cancellationToken)
        => await _termsService.DeleteAsync(request, cancellationToken);
}
