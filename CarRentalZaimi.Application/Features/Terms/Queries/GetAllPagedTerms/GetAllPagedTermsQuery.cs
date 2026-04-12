using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;

namespace CarRentalZaimi.Application.Features.Terms.Queries.GetAllPagedTerms;

public class GetAllPagedTermsQuery : IQuery<PagedResponse<TermsDto>>
{
    public string? Search { get; set; }
    public int PageNr { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
