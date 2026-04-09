using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;

namespace CarRentalZaimi.Application.Features.AdditionalService.Queries.GetAllAdditionalServices;

public class GetAllAdditionalServicesQuery : IQuery<IEnumerable<AdditionalServiceDto>>;
