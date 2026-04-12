using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;

namespace CarRentalZaimi.Application.Features.Partner.Queries.GetAllPartners;

public class GetAllPartnersQuery() : IQuery<IEnumerable<PartnerDto>>;
