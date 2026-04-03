using AutoMapper;
using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Common.Errors;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.Users.Commands.AddPhoneNumber;
using CarRentalZaimi.Application.Features.Users.Commands.UpdateUser;
using CarRentalZaimi.Application.Features.Users.Queries;
using CarRentalZaimi.Application.Interfaces.Services;
using CarRentalZaimi.Application.Interfaces.UnitOfWork;
using CarRentalZaimi.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace CarRentalZaimi.Application.Services;

public class UserService : IUserService
{
    private readonly IErrorService _errorService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly IMapper _mapper;

    public UserService(
        IErrorService errorService, 
        IUnitOfWork unitOfWork,
        UserManager<User> userManager,
        RoleManager<Role> roleManager,
        IMapper mapper)
    {
        _errorService = errorService;
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _roleManager = roleManager;
        _mapper = mapper;
    }

    public async Task<Result<UserDto>> GetUserByIdAsync(GetUserByIdQuery request, CancellationToken cancellationToken = default)
    {
        var user = await _unitOfWork.Repository<User>()
            .FirstOrDefaultAsync(p => p.Id == request.UserId, cancellationToken);

        if (user is null)
            return _errorService.CreateFailure<UserDto>(ErrorCodes.NOT_FOUND);


        var userImage = await _unitOfWork.Repository<UserImage>()
            .FirstOrDefaultAsync(p => p.User!.Id == request.UserId, cancellationToken);


        var response = new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            DateOfBirth = user.DateOfBirth,
            Email = user.Email,
            Username = user.UserName,
            PhoneNumber = user.PhoneNumber,
            Role =  await GetRoleDtoAsync(user),
            Image = _mapper.Map<UserImageDto>(userImage)
        };

        return Result<UserDto>.Success(response);
    }

    public async Task<Result<UserDto>> UpdateUserProfileAsync(UpdateUserCommand command, CancellationToken cancellationToken = default)
    {
        var user = await _unitOfWork.Repository<User>()
        .FirstOrDefaultAsync(p => p.Id == command.UserId, cancellationToken);

        if (user is null)
            return _errorService.CreateFailure<UserDto>(ErrorCodes.NOT_FOUND);

        if (!string.IsNullOrWhiteSpace(command.Firstname))
            user.FirstName = command.Firstname;

        if (!string.IsNullOrWhiteSpace(command.Lastname))
            user.LastName = command.Lastname;

        if (command.DateOfBirth.HasValue)
            user.DateOfBirth = command.DateOfBirth.Value;

        await _unitOfWork.Repository<User>().UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var userImage = await _unitOfWork.Repository<UserImage>()
            .FirstOrDefaultAsync(p => p.User!.Id == command.UserId, cancellationToken);

        var response = new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            DateOfBirth = user.DateOfBirth,
            Email = user.Email,
            Username = user.UserName,
            PhoneNumber = user.PhoneNumber,
            Role = await GetRoleDtoAsync(user),
            Image = _mapper.Map<UserImageDto>(userImage)
        };

        return Result<UserDto>.Success(response);
    }

    public async Task<Result<UserDto>> AddPhoneNumberAsync(AddPhoneNumberCommand command, CancellationToken cancellationToken = default)
    {
        var user = await _unitOfWork.Repository<User>()
            .FirstOrDefaultAsync(p => p.Id == command.UserId, cancellationToken);

        if (user is null)
            return _errorService.CreateFailure<UserDto>(ErrorCodes.NOT_FOUND);

        var phoneExists = await _unitOfWork.Repository<User>()
             .AnyAsync(p => p.PhoneNumber == command.PhoneNumber && p.Id != command.UserId, cancellationToken);

        if (phoneExists)
            return _errorService.CreateFailure<UserDto>(ErrorCodes.USER_PHONE_ALREADY_EXISTS);

        user.PhoneNumber = command.PhoneNumber;

        await _unitOfWork.Repository<User>().UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            DateOfBirth = user.DateOfBirth,
            Email = user.Email,
            Username = user.UserName,
            PhoneNumber = user.PhoneNumber,
            Role = await GetRoleDtoAsync(user)
        };

        return Result<UserDto>.Success(response);
    }


    private async Task<RoleDto?> GetRoleDtoAsync(User user)
    {
        var names = await _userManager.GetRolesAsync(user);
        var name = names.FirstOrDefault();
        if (name == null) return null;
        var role = await _roleManager.FindByNameAsync(name);
        return role != null ? _mapper.Map<RoleDto>(role) : null;
    }
}
