using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.Partner.Commands.CreatePartner;
using CarRentalZaimi.Application.Features.Partner.Commands.DeletePartner;
using CarRentalZaimi.Application.Features.Partner.Commands.UpdatePartner;
using CarRentalZaimi.Application.Features.Partner.Queries.GetAllPagedPartners;
using CarRentalZaimi.Application.Features.Partner.Queries.GetAllPartners;

namespace CarRentalZaimi.Application.Interfaces.Services;

public interface IPartnerService
{
    Task<Result<PartnerDto>> CreateAsync(CreatePartnerCommand request, CancellationToken cancellationToken = default);
    Task<Result<PartnerDto>> UpdateAsync(UpdatePartnerCommand request, CancellationToken cancellationToken = default);
    Task<Result<bool>> DeleteAsync(DeletePartnerCommand request, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<PartnerDto>>> GetAllAsync(GetAllPartnersQuery request, CancellationToken cancellationToken = default);
    Task<Result<PagedResponse<PartnerDto>>> GetAllPagedAsync(GetAllPagedPartnersQuery request, CancellationToken cancellationToken = default);
}
