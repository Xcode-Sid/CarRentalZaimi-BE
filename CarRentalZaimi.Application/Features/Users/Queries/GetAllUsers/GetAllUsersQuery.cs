using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;

namespace CarRentalZaimi.Application.Features.Users.Queries.GetAllUsers;

public class GetAllUsersQuery : IQuery<PagedResponse<UserDto>>
{
    public string? Status { get; set; }
    public string? Search { get; set; }
    public int PageNr { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
