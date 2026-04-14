using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;

namespace CarRentalZaimi.Application.Features.AdditionalService.Queries.GetAllPagedAdditionalServices;

public class GetAllPagedAdditionalServicesQuery : IQuery<PagedResponse<AdditionalServiceDto>>
{
    public string? Search { get; set; }
    public int PageNr { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
