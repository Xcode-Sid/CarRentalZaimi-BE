using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.Users.Commands.AddPhoneNumber;

public class AddPhoneNumberCommandHandler(IUserService _userService) : ICommandHandler<AddPhoneNumberCommand, UserDto>
{
    public async Task<ApiResponse<UserDto>> Handle(AddPhoneNumberCommand request, CancellationToken cancellationToken)
      => await _userService.AddPhoneNumberAsync(request, cancellationToken);
}
