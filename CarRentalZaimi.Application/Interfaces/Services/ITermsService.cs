using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.Terms.Commands.CreateTerm;
using CarRentalZaimi.Application.Features.Terms.Commands.DeleteTerm;
using CarRentalZaimi.Application.Features.Terms.Commands.UpdateTerm;
using CarRentalZaimi.Application.Features.Terms.Queries.GetAllPagedTerms;
using CarRentalZaimi.Application.Features.Terms.Queries.GetAllTerms;

namespace CarRentalZaimi.Application.Interfaces.Services;

public interface ITermsService
{
    Task<Result<TermsDto>> CreateAsync(CreateTermCommand request, CancellationToken cancellationToken = default);
    Task<Result<TermsDto>> UpdateAsync(UpdateTermCommand request, CancellationToken cancellationToken = default);
    Task<Result<bool>> DeleteAsync(DeleteTermCommand request, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<TermsDto>>> GetAllAsync(GetAllTermsQuery request, CancellationToken cancellationToken = default);
    Task<Result<PagedResponse<TermsDto>>> GetAllPagedAsync(GetAllPagedTermsQuery request, CancellationToken cancellationToken = default);
}
