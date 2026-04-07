using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Features.StatePrefixes.Commands.CreateStatePrefix;
using CarRentalZaimi.Application.Features.StatePrefixes.Commands.DeleteStatePrefix;
using CarRentalZaimi.Application.Features.StatePrefixes.Commands.UpdateStatePrefix;
using CarRentalZaimi.Application.Features.StatePrefixes.Queries;

namespace CarRentalZaimi.Application.Interfaces.Services;

public interface IStatePrefixService
{
    Task<ApiResponse<StatePrefixDto>> CreateAsync(CreateStatePrefixCommand request, CancellationToken cancellationToken = default);
    Task<ApiResponse<StatePrefixDto>> UpdateAsync(UpdateStatePrefixCommand request, CancellationToken cancellationToken = default);
    Task<ApiResponse<bool>> DeleteAsync(DeleteStatePrefixCommand request, CancellationToken cancellationToken = default);
    Task<ApiResponse<IEnumerable<StatePrefixDto>>> GetAllAsync(GetAllStatePrefixesQuery request, CancellationToken cancellationToken = default);
}
