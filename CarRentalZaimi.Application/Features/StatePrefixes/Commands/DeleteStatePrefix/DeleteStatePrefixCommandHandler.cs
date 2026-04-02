using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.StatePrefixes.Commands.DeleteStatePrefix;

public class DeleteStatePrefixCommandHandler(IStatePrefixService _statePrefixService) : ICommandHandler<DeleteStatePrefixCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteStatePrefixCommand request, CancellationToken cancellationToken)
        => await _statePrefixService.DeleteAsync(request, cancellationToken);
}