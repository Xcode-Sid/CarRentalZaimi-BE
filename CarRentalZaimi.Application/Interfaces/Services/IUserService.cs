using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Features.Users.Commands.AddPhoneNumber;
using CarRentalZaimi.Application.Features.Users.Commands.UpdateUser;
using CarRentalZaimi.Application.Features.Users.Queries.GetUserByEmail;
using CarRentalZaimi.Application.Features.Users.Queries.GetUserById;

namespace CarRentalZaimi.Application.Interfaces.Services;

public interface IUserService
{
    Task<ApiResponse<UserDto>> GetUserByIdAsync(GetUserByIdQuery request, CancellationToken cancellationToken = default);
    Task<ApiResponse<UserDto>> GetUserByEmailAsync(GetUserByEmailQuery request, CancellationToken cancellationToken = default);
    Task<ApiResponse<UserDto>> UpdateUserProfileAsync(UpdateUserCommand command, CancellationToken cancellationToken = default);
    Task<ApiResponse<UserDto>> AddPhoneNumberAsync(AddPhoneNumberCommand command, CancellationToken cancellationToken = default);

}
