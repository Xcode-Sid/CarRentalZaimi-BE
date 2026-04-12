using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.Privacy.Commands.CreatePrivacy;
using CarRentalZaimi.Application.Features.Privacy.Commands.DeletePrivacy;
using CarRentalZaimi.Application.Features.Privacy.Commands.UpdatePrivacy;
using CarRentalZaimi.Application.Features.Privacy.Queries.GetAllPagedPrivacies;
using CarRentalZaimi.Application.Features.Privacy.Queries.GetAllPrivacies;

namespace CarRentalZaimi.Application.Interfaces.Services;

public interface IPrivacyService
{
    Task<Result<PrivacyDto>> CreateAsync(CreatePrivacyCommand request, CancellationToken cancellationToken = default);
    Task<Result<PrivacyDto>> UpdateAsync(UpdatePrivacyCommand request, CancellationToken cancellationToken = default);
    Task<Result<bool>> DeleteAsync(DeletePrivacyCommand request, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<PrivacyDto>>> GetAllAsync(GetAllPrivaciesQuery request, CancellationToken cancellationToken = default);
    Task<Result<PagedResponse<PrivacyDto>>> GetAllPagedAsync(GetAllPagedPrivaciesQuery request, CancellationToken cancellationToken = default);
}
