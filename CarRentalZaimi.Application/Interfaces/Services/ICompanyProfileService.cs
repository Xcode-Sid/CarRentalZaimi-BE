using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.CompanyProfile.Commands.AddCompanyProfileData;
using CarRentalZaimi.Application.Features.CompanyProfile.Queries.GetCompanyProfileData;

namespace CarRentalZaimi.Application.Interfaces.Services
{
    public interface ICompanyProfileService 
    {
        Task<Result<CompanyProfileDto>> AddCompanyProfileDataAsync(AddCompanyProfileDataCommand request, CancellationToken cancellationToken = default);
        Task<Result<CompanyProfileDto>> GetCompanyProfileDataAsync(GetCompanyProfileDataQuery request, CancellationToken cancellationToken = default);
    }
}
