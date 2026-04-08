using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Interfaces.Query;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.Users.Queries.GetUserByEmail;

public class GetUserByEmailQueryHandler(IUserService _userService) : IQueryHandler<GetUserByEmailQuery, UserDto>
{
    public async Task<ApiResponse<UserDto>> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        => await _userService.GetUserByEmailAsync(request, cancellationToken);
}
