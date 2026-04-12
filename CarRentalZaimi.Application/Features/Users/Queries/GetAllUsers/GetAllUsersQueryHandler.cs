using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.Users.Queries.GetAllUsers;

internal class GetAllUsersQueryHandler(IUserService _userService) : IQueryHandler<GetAllUsersQuery, PagedResponse<UserDto>>
{
    public async Task<Result<PagedResponse<UserDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        => await _userService.GetAllUsersAsync(request, cancellationToken);
}
