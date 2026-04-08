using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Interfaces.Query;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.Users.Queries.GetUserById;

public class GetUserByIdQueryHandler(IUserService _userProfileService) : IQueryHandler<GetUserByIdQuery, UserDto>
{
    public async Task<ApiResponse<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        => await _userProfileService.GetUserByIdAsync(request, cancellationToken);
}
