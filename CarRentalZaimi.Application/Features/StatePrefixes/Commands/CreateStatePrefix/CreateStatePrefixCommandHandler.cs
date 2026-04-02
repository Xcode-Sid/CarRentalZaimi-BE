using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.StatePrefixes.Commands.CreateStatePrefix;

public class CreateStatePrefixCommandHandler(IStatePrefixService _statePrefixService) : ICommandHandler<CreateStatePrefixCommand, StatePrefixDto>
{
    public async Task<Result<StatePrefixDto>> Handle(CreateStatePrefixCommand request, CancellationToken cancellationToken)
        => await _statePrefixService.CreateAsync(request, cancellationToken);
}

