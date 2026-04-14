using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.CompanyProfile.Commands.AddCompanyProfileData;

internal class AddCompanyProfileDataCommandHandler(ICompanyProfileService _companyProfileService) : ICommandHandler<AddCompanyProfileDataCommand, CompanyProfileDto>
{
    public async Task<Result<CompanyProfileDto>> Handle(AddCompanyProfileDataCommand request, CancellationToken cancellationToken)
        => await _companyProfileService.AddCompanyProfileDataAsync(request, cancellationToken);
}
