using AutoMapper;
using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Common.Errors;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.Users.Commands.AddPhoneNumber;
using CarRentalZaimi.Application.Features.Users.Commands.UpdateUser;
using CarRentalZaimi.Application.Features.Users.Queries.GetAllUsers;
using CarRentalZaimi.Application.Features.Users.Queries.GetUserByEmail;
using CarRentalZaimi.Application.Features.Users.Queries.GetUserById;
using CarRentalZaimi.Application.Interfaces.Services;
using CarRentalZaimi.Application.Interfaces.UnitOfWork;
using CarRentalZaimi.Domain.Entities;
using CarRentalZaimi.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CarRentalZaimi.Application.Services;

public class UserService : IUserService
{
    private readonly IErrorService _errorService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly IMapper _mapper;
    private readonly INotificationService _notificationService;

    public UserService(
        IErrorService errorService, 
        IUnitOfWork unitOfWork,
        UserManager<User> userManager,
        RoleManager<Role> roleManager,
        IMapper mapper,
        INotificationService notificationService)
    {
        _errorService = errorService;
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _roleManager = roleManager;
        _mapper = mapper;
        _notificationService = notificationService;
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
            FirstName = user.FirstName,
            LastName = user.LastName,
            DateOfBirth = user.DateOfBirth,
            Email = user.Email,
            Username = user.UserName,
            PhoneNumber = user.PhoneNumber,
            Location = user.Location,
            Role =  await GetRoleDtoAsync(user),
            Image = _mapper.Map<UserImageDto>(userImage),
            Status = user.Status.ToString()
        };

        return Result<UserDto>.Success(response);
    }


    public async Task<Result<PagedResponse<UserDto>>> GetAllUsersAsync(GetAllUsersQuery request, CancellationToken cancellationToken = default)
    {
        var query = _unitOfWork.Repository<User>()
            .AsQueryable()
            .Include(u => u.Image)
            .Where(c => !c.IsDeleted);

        // Search
        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.ToLower();
            query = query.Where(c =>
                (c.FirstName != null && c.FirstName.ToLower().Contains(search)) ||
                (c.LastName != null && c.LastName.ToLower().Contains(search)) ||
                (c.UserName != null && c.UserName.ToLower().Contains(search)) ||
                (c.Email != null && c.Email.ToLower().Contains(search)));
        }


        if (Enum.TryParse<UserStatus>(request.Status, ignoreCase: true, out var status))
            query = query.Where(c => c.Status == status);


        var totalCount = await query.CountAsync(cancellationToken);

        var cars = await query
            .Skip((request.PageNr - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var mapped = _mapper.Map<List<UserDto>>(cars);
        var pagedResponse = new PagedResponse<UserDto>(mapped, totalCount, request.PageNr, request.PageSize);
        return Result<PagedResponse<UserDto>>.Success(pagedResponse);
    }

    public async Task<Result<UserDto>> GetUserByEmailAsync(GetUserByEmailQuery request, CancellationToken cancellationToken = default)
    {
        var user = await _unitOfWork.Repository<User>()
            .FirstOrDefaultAsync(p => p.Email == request.Email, cancellationToken);

        if (user is null)
            return _errorService.CreateFailure<UserDto>(ErrorCodes.NOT_FOUND);


        var userImage = await _unitOfWork.Repository<UserImage>()
            .FirstOrDefaultAsync(p => p.User!.Email == request.Email, cancellationToken);


        var response = new UserDto
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            DateOfBirth = user.DateOfBirth,
            Email = user.Email,
            Username = user.UserName,
            PhoneNumber = user.PhoneNumber,
            Location = user.Location,
            Role =  await GetRoleDtoAsync(user),
            Image = _mapper.Map<UserImageDto>(userImage),
            Status = user.Status.ToString(),
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

        if (!string.IsNullOrWhiteSpace(command.Location))
            user.Location = command.Location;

        if (command.DateOfBirth.HasValue)
            user.DateOfBirth = command.DateOfBirth.Value;

        // Handle profile image update
        if (!string.IsNullOrWhiteSpace(command.Name) && !string.IsNullOrWhiteSpace(command.Data))
        {
            var existingImage = await _unitOfWork.Repository<UserImage>()
                .FirstOrDefaultAsync(p => p.User!.Id == command.UserId, cancellationToken);

            if (existingImage is not null)
            {
                var oldImageFullPath = Path.Combine("wwwroot", existingImage.ImagePath!);
                if (File.Exists(oldImageFullPath))
                    File.Delete(oldImageFullPath);

                await _unitOfWork.Repository<UserImage>().DeleteAsync(existingImage);
            }

            var folderName = $"{user.UserName}_{user.Id}";
            var folderPath = Path.Combine("wwwroot", "images", folderName);
            Directory.CreateDirectory(folderPath);

            byte[] imageData = Convert.FromBase64String(command.Data);
            var imagePath = Path.Combine(folderPath, command.Name);

            await using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await fileStream.WriteAsync(imageData, 0, imageData.Length);
            }

            var profileImage = new UserImage
            {
                User = user,
                ImageName = command.Name,
                ImagePath = $"images/{folderName}/{command.Name}",
            };

            await _unitOfWork.Repository<UserImage>().AddAsync(profileImage);
        }

        await _unitOfWork.Repository<User>().UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _notificationService.SendNotificationToAdminsAsync($"User {user.FirstName} {user.LastName} updated their profile.", UserNotificationType.ProfileUpdated);

        var userImage = await _unitOfWork.Repository<UserImage>()
            .FirstOrDefaultAsync(p => p.User!.Id == command.UserId, cancellationToken);

        var response = new UserDto
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            DateOfBirth = user.DateOfBirth,
            Email = user.Email,
            Username = user.UserName,
            PhoneNumber = user.PhoneNumber,
            Location = user.Location,
            Role = await GetRoleDtoAsync(user),
            Image = _mapper.Map<UserImageDto>(userImage),
            Status = user.Status.ToString()
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

        await _notificationService.SendNotificationToAdminsAsync($"User {user.FirstName} {user.LastName} added a phone number.", UserNotificationType.ProfileUpdated);

        var response = new UserDto
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            DateOfBirth = user.DateOfBirth,
            Email = user.Email,
            Username = user.UserName,
            PhoneNumber = user.PhoneNumber,
            Location = user.Location,
            Role = await GetRoleDtoAsync(user),
            Status = user.Status.ToString()
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
