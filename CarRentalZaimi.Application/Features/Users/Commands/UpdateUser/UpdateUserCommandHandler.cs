using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler(IUserService _userService) : ICommandHandler<UpdateUserCommand, UserDto>
{
    public async Task<ApiResponse<UserDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        => await _userService.UpdateUserProfileAsync(request, cancellationToken);
}
