using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.StatePrefixes.Commands.CreateStatePrefix;
using CarRentalZaimi.Application.Features.StatePrefixes.Commands.DeleteStatePrefix;
using CarRentalZaimi.Application.Features.StatePrefixes.Commands.UpdateStatePrefix;
using CarRentalZaimi.Application.Features.StatePrefixes.Queries.GetAllPagedStatePrefixes;
using CarRentalZaimi.Application.Features.StatePrefixes.Queries.GetAllStatePrefixes;

namespace CarRentalZaimi.Application.Interfaces.Services;

public interface IStatePrefixService
{
    Task<Result<StatePrefixDto>> CreateAsync(CreateStatePrefixCommand request, CancellationToken cancellationToken = default);
    Task<Result<StatePrefixDto>> UpdateAsync(UpdateStatePrefixCommand request, CancellationToken cancellationToken = default);
    Task<Result<bool>> DeleteAsync(DeleteStatePrefixCommand request, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<StatePrefixDto>>> GetAllAsync(GetAllStatePrefixesQuery request, CancellationToken cancellationToken = default);
    Task<Result<PagedResponse<StatePrefixDto>>> GetAllPagedAsync(GetAllPagedStatePrefixesQuery request, CancellationToken cancellationToken = default);
}
