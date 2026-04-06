using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.Users.Commands.AddPhoneNumber;
using CarRentalZaimi.Application.Features.Users.Commands.UpdateUser;
using CarRentalZaimi.Application.Features.Users.Queries.GetUserByEmail;
using CarRentalZaimi.Application.Features.Users.Queries.GetUserById;

namespace CarRentalZaimi.Application.Interfaces.Services;

public interface IUserService
{
    Task<Result<UserDto>> GetUserByIdAsync(GetUserByIdQuery request, CancellationToken cancellationToken = default);
    Task<Result<UserDto>> GetUserByEmailAsync(GetUserByEmailQuery request, CancellationToken cancellationToken = default);
    Task<Result<UserDto>> UpdateUserProfileAsync(UpdateUserCommand command, CancellationToken cancellationToken = default);
    Task<Result<UserDto>> AddPhoneNumberAsync(AddPhoneNumberCommand command, CancellationToken cancellationToken = default);

}
