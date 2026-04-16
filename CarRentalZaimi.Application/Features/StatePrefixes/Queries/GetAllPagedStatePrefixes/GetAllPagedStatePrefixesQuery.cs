using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;

namespace CarRentalZaimi.Application.Features.StatePrefixes.Queries.GetAllPagedStatePrefixes;

public class GetAllPagedStatePrefixesQuery : IQuery<PagedResponse<StatePrefixDto>>
{
    public string? Search { get; set; }
    public int PageNr { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
