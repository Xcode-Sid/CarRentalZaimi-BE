using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.StatePrefixes.Commands.UpdateStatePrefix;

public class UpdateStatePrefixCommandHandler(IStatePrefixService _statePrefixService) : ICommandHandler<UpdateStatePrefixCommand, StatePrefixDto>
{
    public async Task<ApiResponse<StatePrefixDto>> Handle(UpdateStatePrefixCommand request, CancellationToken cancellationToken)
        => await _statePrefixService.UpdateAsync(request, cancellationToken);
}
