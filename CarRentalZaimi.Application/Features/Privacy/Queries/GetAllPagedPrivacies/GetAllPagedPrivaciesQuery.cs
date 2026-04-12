using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;

namespace CarRentalZaimi.Application.Features.Privacy.Queries.GetAllPagedPrivacies;

public class GetAllPagedPrivaciesQuery : IQuery<PagedResponse<PrivacyDto>>
{
    public string? Search { get; set; }
    public int PageNr { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

