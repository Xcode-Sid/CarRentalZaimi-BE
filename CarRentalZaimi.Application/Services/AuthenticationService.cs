using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Common.Constants;
using CarRentalZaimi.Application.Common.Errors;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Repositories;
using CarRentalZaimi.Application.Interfaces.Services;
using CarRentalZaimi.Application.Interfaces.UnitOfWork;
using CarRentalZaimi.Domain.Common.Constants;
using CarRentalZaimi.Domain.Entities;
using CarRentalZaimi.Domain.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace CarRentalZaimi.Application.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly IErrorService _errorService;
    private readonly IPasswordService _passwordService;
    private readonly ILogger<AuthenticationService> _logger;
    private readonly IJwtTokenService _jwtTokenService; 
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;

    public AuthenticationService(
        IUserRepository userRepository, 
        IErrorService errorService, 
        IPasswordService passwordService,
        ILogger<AuthenticationService> logger,
        IUnitOfWork unitOfWork,
        IConfiguration configuration,
        IJwtTokenService jwtTokenService)
    {
        _userRepository = userRepository;   
        _errorService = errorService;
        _passwordService = passwordService;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _configuration = configuration;
        _jwtTokenService = jwtTokenService;
    }
    public async Task<Result<UserDto>> RegisterAsync(string firstname, string lastname, DateTime? dateOfBirth, string username, string email, 
        string phone, string password, string? name, string? data, string? deviceInfo = null)
    {
        try
        {
            // Check if user already exists (GetByEmailAsync already filters out deleted users)
            var existingUser = await _userRepository.GetByEmailAsync(email);
            if (existingUser != null)
                return _errorService.CreateFailure<UserDto>(ErrorCodes.USER_EMAIL_ALREADY_EXISTS);

            // Validate password strength
            if (!_passwordService.IsPasswordStrong(password))
                return _errorService.CreateFailure<UserDto>(ErrorCodes.VALIDATION_FAILED);


            // Create new user with PendingVerification status (will be activated after email confirmation)
            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = firstname,
                LastName = lastname,
                DateOfBirth =  dateOfBirth,
                Email = email,
                UserName = username,
                PhoneNumber = phone,
                PasswordHash = _passwordService.HashPassword(password),
                Status = UserStatus.PendingVerification, // New users start as PendingVerification
                EmailConfirmed = false
            };

            await _userRepository.AddAsync(user);

            await _unitOfWork.SaveChangesAsync();


            // Save profile image if provided
            if (!string.IsNullOrEmpty(data) && !string.IsNullOrEmpty(name))
            {
                var folderName = $"{user.UserName}_{user.Id}";
                var folderPath = Path.Combine("wwwroot", "images", folderName);
                Directory.CreateDirectory(folderPath);

                byte[] imageData = Convert.FromBase64String(data);
                var imagePath = Path.Combine(folderPath, name);

                await using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    await fileStream.WriteAsync(imageData, 0, imageData.Length);
                }

                var profileImage = new UserImage
                {
                    User = user,
                    ImageName = name,
                    ImagePath = $"images/{folderName}/{name}",
                };

                await _unitOfWork.Repository<UserImage>().AddAsync(profileImage);
            }

            await _unitOfWork.SaveChangesAsync();

            // Generate JWT token
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new(ClaimTypes.Role, user.Status.ToString()),
                new(ClaimNames.UserStatus, user.Status.ToString())
            };

            var jwtSettings = _configuration.GetSection(ConfigurationKeys.Sections.JwtSettings);
            var accessTokenExpiryMinutes = int.Parse(jwtSettings[ConfigurationKeys.JwtSettingKeys.ExpiryMinutes] ?? "60");
            var refreshTokenExpiryDays = int.Parse(jwtSettings[ConfigurationKeys.JwtSettingKeys.RefreshTokenExpiryDays] ?? "7");

            var accessToken = _jwtTokenService.GenerateAccessToken(claims);
            var refreshTokenValue = _jwtTokenService.GenerateRefreshToken();
            var accessTokenExpiresAt = DateTime.UtcNow.AddMinutes(accessTokenExpiryMinutes);
            var refreshTokenExpiresAt = DateTime.UtcNow.AddDays(refreshTokenExpiryDays);

            // Save refresh token to database
            var refreshToken = new RefreshToken
            {
                User = user,
                Token = refreshTokenValue,
                ExpiresAt = refreshTokenExpiresAt,
                IsRevoked = false,
                DeviceInfo = deviceInfo,
            };

            await _userRepository.AddRefreshTokenAsync(refreshToken);

            _logger.LogInformation("User {UserId} registered successfully", user.Id);


            var response = new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                Username = user.UserName,
                PhoneNumber = user.PhoneNumber,
            };

            return Result<UserDto>.Success(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Registration failed for email {Email}", email);
            return _errorService.CreateFailure<UserDto>(ErrorCodes.EXTERNAL_SERVICE_ERROR);
        }
    }
}
