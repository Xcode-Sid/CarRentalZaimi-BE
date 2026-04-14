using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.CompanyProfile.Queries.GetCompanyProfileData;

internal class GetCompanyProfileDataQueryHandler(ICompanyProfileService _companyProfileService) : IQueryHandler<GetCompanyProfileDataQuery, CompanyProfileDto>
{
    public async Task<Result<CompanyProfileDto>> Handle(GetCompanyProfileDataQuery request, CancellationToken cancellationToken)
        => await _companyProfileService.GetCompanyProfileDataAsync(request, cancellationToken);
}
