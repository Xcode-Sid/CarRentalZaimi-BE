using AutoMapper;
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
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;
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
    private readonly UserManager<User> _userManager; 
    private readonly RoleManager<Role> _roleManager;
    private readonly IMapper _mapper;

    public AuthenticationService(
        IUserRepository userRepository, 
        IErrorService errorService, 
        IPasswordService passwordService,
        ILogger<AuthenticationService> logger,
        IUnitOfWork unitOfWork,
        IConfiguration configuration,
        IJwtTokenService jwtTokenService,
        UserManager<User> userManager,
        RoleManager<Role> roleManager,
        IMapper mapper)
    {
        _userRepository = userRepository;   
        _errorService = errorService;
        _passwordService = passwordService;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _configuration = configuration;
        _jwtTokenService = jwtTokenService;
        _userManager = userManager;
        _roleManager = roleManager;
        _mapper = mapper;
    }

    public async Task<Result<UserDto>> RegisterAsync(string firstname, string lastname, DateTime? dateOfBirth, string username, string email, 
        string phone, string password, string? name, string? data, string? role, string? location, string? deviceInfo = null)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            // Check if user already exists (GetByEmailAsync already filters out deleted users)
            var existingUser = await _userRepository.GetByEmailAsync(email);
            if (existingUser != null)
                return _errorService.CreateFailure<UserDto>(ErrorCodes.USER_EMAIL_ALREADY_EXISTS);

            var existingUserByPhone = await _userRepository.GetByPhoneAsync(phone);
            if (existingUserByPhone != null)
                return _errorService.CreateFailure<UserDto>(ErrorCodes.USER_PHONE_ALREADY_EXISTS);


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
                EmailConfirmed = false,
                Location = location,
            };

            await _userRepository.AddAsync(user);

            await _unitOfWork.SaveChangesAsync();


            // Save profile image if provided
            if (!string.IsNullOrEmpty(data) && !string.IsNullOrEmpty(name))
                await SaveProfileImageAsync(user, name, data);

            await _unitOfWork.SaveChangesAsync();

            // Generate JWT token
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new(ClaimTypes.Role, SystemPolicies.User.ToString()),
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
            if (role != null)
            {

                var roleResult = await _userManager.AddToRoleAsync(user, role);
                if (!roleResult.Succeeded)
                {
                    _logger.LogError("Failed to assign role {Role} to user {UserId}", role, user.Id);
                    return _errorService.CreateFailure<UserDto>(ErrorCodes.VALIDATION_FAILED);
                }
            }

            _logger.LogInformation("User {UserId} registered successfully", user.Id);
            await _unitOfWork.CommitTransactionAsync();

            var response = new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                Username = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Location = user.Location,
                Role =  await GetRoleDtoAsync(user),
            };

            return Result<UserDto>.Success(response);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            _logger.LogError(ex, "Registration failed for email {Email}", email); 
            return _errorService.CreateFailure<UserDto>(ErrorCodes.EXTERNAL_SERVICE_ERROR);
        }
    }

    public async Task<Result<AuthenticationResponseDto>> AuthenticateWithGoogleAsync(string? email, string? firstName, string? lastName, string? picture, 
        string? externalProviderId, string? deviceInfo = null, CancellationToken cancellationToken = default)
    {
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            if (string.IsNullOrEmpty(email))
                return _errorService.CreateFailure<AuthenticationResponseDto>(ErrorCodes.VALIDATION_FAILED);

            var user = await _userRepository.GetByEmailAsync(email);

            if (user == null)
            {

                user = new User
                {
                    Id = Guid.NewGuid().ToString(),
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email.Split('@')[0],
                    PasswordHash = null, // External auth users have no password
                    Status = UserStatus.Active, // Google-verified users are active immediately
                    ExternalProvider = "Google",
                    ExternalProviderId = externalProviderId,
                    EmailConfirmed = true, // Google already confirmed the email
                };

                await _userRepository.AddAsync(user);
                await _unitOfWork.SaveChangesAsync();


                //add automatiacaly user role
                string role = SystemPolicies.User;
                var roleResult = await _userManager.AddToRoleAsync(user, role);
                if (!roleResult.Succeeded)
                {
                    _logger.LogError("Failed to assign role {Role} to user {UserId}", role, user.Id);
                    return _errorService.CreateFailure<AuthenticationResponseDto>(ErrorCodes.VALIDATION_FAILED);
                }

                // Download profile picture and convert to base64
                if (!string.IsNullOrEmpty(picture))
                {
                    try
                    {
                        using var httpClient = new HttpClient();
                        var imageBytes = await httpClient.GetByteArrayAsync(picture);
                        string? base64Image = Convert.ToBase64String(imageBytes);
                        // Save profile image if provided
                        var name = $"profile_{user.Id}.jpg";
                        if (!string.IsNullOrEmpty(picture) && !string.IsNullOrEmpty(name))
                            await SaveProfileImageAsync(user, name, base64Image);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "Failed to download Facebook profile picture for {Email}", email);
                    }
                }

                await _unitOfWork.SaveChangesAsync();
                _logger.LogInformation("New Google user {UserId} created successfully", user.Id);
            }
            else
            {
                // Existing user - check if they can log in
                if (user.Status == UserStatus.Banned)
                    return _errorService.CreateFailure<AuthenticationResponseDto>(ErrorCodes.USER_BANNED);

                if (user.Status == UserStatus.Suspended)
                    return _errorService.CreateFailure<AuthenticationResponseDto>(ErrorCodes.USER_SUSPENDED);

                if (user.Status != UserStatus.Active)
                    return _errorService.CreateFailure<AuthenticationResponseDto>(ErrorCodes.USER_INACTIVE);
            }

            
            // Generate tokens
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new(ClaimTypes.Role, SystemPolicies.User.ToString()),
                new(ClaimNames.UserStatus, user.Status.ToString())
            };

            var jwtSettings = _configuration.GetSection("JwtSettings");
            var accessTokenExpiryMinutes = int.Parse(jwtSettings["ExpiryMinutes"] ?? "60");
            var refreshTokenExpiryDays = int.Parse(jwtSettings["RefreshTokenExpiryDays"] ?? "7");

            var accessToken = _jwtTokenService.GenerateAccessToken(claims);
            var refreshTokenValue = _jwtTokenService.GenerateRefreshToken();
            var accessTokenExpiresAt = DateTime.UtcNow.AddMinutes(accessTokenExpiryMinutes);
            var refreshTokenExpiresAt = DateTime.UtcNow.AddDays(refreshTokenExpiryDays);

            // Save refresh token
            var refreshToken = new RefreshToken
            {
                User = user,
                Token = refreshTokenValue,
                ExpiresAt = refreshTokenExpiresAt,
                IsRevoked = false,
                DeviceInfo = deviceInfo,
            };

            await _userRepository.UpdateAsync(user);
            await _userRepository.AddRefreshTokenAsync(refreshToken);
            await _unitOfWork.CommitTransactionAsync();
            _logger.LogInformation("Google user {UserId} authenticated successfully", user.Id);

            return Result<AuthenticationResponseDto>.Success(new AuthenticationResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshTokenValue,
                AccessTokenExpiresAt = accessTokenExpiresAt,
                RefreshTokenExpiresAt = refreshTokenExpiresAt,
                User = MapToUserDto(user),
                Role = await GetRoleDtoAsync(user)
            });
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            _logger.LogError(ex, "Google authentication failed for email {Email}", email);
            return _errorService.CreateFailure<AuthenticationResponseDto>(ErrorCodes.EXTERNAL_SERVICE_ERROR);
        }
    }


    public async Task<Result<AuthenticationResponseDto>> AuthenticateWithFacebookAsync(string? email, string? firstName, string? lastName, string? picture, 
        string? externalProviderId, string? deviceInfo = null, CancellationToken cancellationToken = default)
    {
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            if (string.IsNullOrEmpty(email))
                return _errorService.CreateFailure<AuthenticationResponseDto>(ErrorCodes.VALIDATION_FAILED);

            var user = await _userRepository.GetByEmailAsync(email);

            if (user == null)
            {
                user = new User
                {
                    Id = Guid.NewGuid().ToString(),
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email.Split('@')[0],
                    PasswordHash = null, // External users have no pass
                    Status = UserStatus.Active, // Facebook-verified users are active immediately
                    ExternalProvider = "Facebook",
                    ExternalProviderId = externalProviderId,
                    EmailConfirmed = true, 
                };


                await _userRepository.AddAsync(user);
                await _unitOfWork.SaveChangesAsync();


                //add automatiacaly user role
                string role = SystemPolicies.User;
                var roleResult = await _userManager.AddToRoleAsync(user, role);
                if (!roleResult.Succeeded)
                {
                    _logger.LogError("Failed to assign role {Role} to user {UserId}", role, user.Id);
                    return _errorService.CreateFailure<AuthenticationResponseDto>(ErrorCodes.VALIDATION_FAILED);
                }


                // Download profile picture and convert to base64
                if (!string.IsNullOrEmpty(picture))
                {
                    try
                    {
                        using var httpClient = new HttpClient();
                        var imageBytes = await httpClient.GetByteArrayAsync(picture);
                        string? base64Image = Convert.ToBase64String(imageBytes);
                        // Save profile image if provided
                        var name = $"profile_{user.Id}.jpg";
                        if (!string.IsNullOrEmpty(picture) && !string.IsNullOrEmpty(name))
                            await SaveProfileImageAsync(user, name, base64Image);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "Failed to download Facebook profile picture for {Email}", email);
                    }
                }

                await _unitOfWork.SaveChangesAsync();
                _logger.LogInformation("New Facebook user {UserId} created successfully", user.Id);
            }
            else
            {
                // Existing user - check if they can log in
                if (user.Status == UserStatus.Banned)
                    return _errorService.CreateFailure<AuthenticationResponseDto>(ErrorCodes.USER_BANNED);

                if (user.Status == UserStatus.Suspended)
                    return _errorService.CreateFailure<AuthenticationResponseDto>(ErrorCodes.USER_SUSPENDED);

                if (user.Status != UserStatus.Active)
                    return _errorService.CreateFailure<AuthenticationResponseDto>(ErrorCodes.USER_INACTIVE);
            }

          
            // Generate tokens
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Email, user.Email!),
                new(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new(ClaimTypes.Role, SystemPolicies.User.ToString()),
                new(ClaimNames.UserStatus, user.Status.ToString())
            };

            var jwtSettings = _configuration.GetSection("JwtSettings");
            var accessTokenExpiryMinutes = int.Parse(jwtSettings["ExpiryMinutes"] ?? "60");
            var refreshTokenExpiryDays = int.Parse(jwtSettings["RefreshTokenExpiryDays"] ?? "7");

            var accessToken = _jwtTokenService.GenerateAccessToken(claims);
            var refreshTokenValue = _jwtTokenService.GenerateRefreshToken();
            var accessTokenExpiresAt = DateTime.UtcNow.AddMinutes(accessTokenExpiryMinutes);
            var refreshTokenExpiresAt = DateTime.UtcNow.AddDays(refreshTokenExpiryDays);

            // Save refresh token
            var refreshToken = new RefreshToken
            {
                User = user,
                Token = refreshTokenValue,
                ExpiresAt = refreshTokenExpiresAt,
                IsRevoked = false,
                DeviceInfo = deviceInfo,
                CreatedOn = DateTime.UtcNow
            };

            await _userRepository.AddRefreshTokenAsync(refreshToken);

            await _userRepository.UpdateAsync(user);
            await _unitOfWork.CommitTransactionAsync();
            _logger.LogInformation("Facebook user {UserId} authenticated successfully", user.Id);

            return Result<AuthenticationResponseDto>.Success(new AuthenticationResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshTokenValue,
                AccessTokenExpiresAt = accessTokenExpiresAt,
                RefreshTokenExpiresAt = refreshTokenExpiresAt,
                User = MapToUserDto(user),
                Role = await GetRoleDtoAsync(user)
            });
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            _logger.LogError(ex, "Facebook authentication failed for email {Email}", email);
            return _errorService.CreateFailure<AuthenticationResponseDto>(ErrorCodes.EXTERNAL_SERVICE_ERROR);
        }
    }

    public async Task<Result<AuthenticationResponseDto>> AuthenticateWithMicrosoftAsync(string? email, string? firstName, string? lastName, string? externalProviderId, 
        string? deviceInfo = null, CancellationToken cancellationToken = default)
    {
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            if (string.IsNullOrEmpty(email))
                return _errorService.CreateFailure<AuthenticationResponseDto>(ErrorCodes.VALIDATION_FAILED);

            var user = await _userRepository.GetByEmailAsync(email);

            if (user == null)
            {
                user = new User
                {
                    Id = Guid.NewGuid().ToString(),
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email.Split('@')[0],
                    PasswordHash = null, // External auth users have no password
                    Status = UserStatus.Active, // Facebook-verified users are active immediately
                    ExternalProvider = "Microsoft",
                    ExternalProviderId = externalProviderId,
                    EmailConfirmed = true, 
                };


                await _userRepository.AddAsync(user);
                await _unitOfWork.SaveChangesAsync();

                //add automatiacaly user role
                string role = SystemPolicies.User;
                var roleResult = await _userManager.AddToRoleAsync(user, role);
                if (!roleResult.Succeeded)
                {
                    _logger.LogError("Failed to assign role {Role} to user {UserId}", role, user.Id);
                    return _errorService.CreateFailure<AuthenticationResponseDto>(ErrorCodes.VALIDATION_FAILED);
                }

                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("New Microsoft user {UserId} created successfully", user.Id);
            }
            else
            {
                // Existing user - check if they can log in
                if (user.Status == UserStatus.Banned)
                    return _errorService.CreateFailure<AuthenticationResponseDto>(ErrorCodes.USER_BANNED);

                if (user.Status == UserStatus.Suspended)
                    return _errorService.CreateFailure<AuthenticationResponseDto>(ErrorCodes.USER_SUSPENDED);

                if (user.Status != UserStatus.Active)
                    return _errorService.CreateFailure<AuthenticationResponseDto>(ErrorCodes.USER_INACTIVE);
            }


            // Generate tokens
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Email, user.Email!),
                new(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new(ClaimTypes.Role, SystemPolicies.User.ToString()),
                new(ClaimNames.UserStatus, user.Status.ToString())
            };

            var jwtSettings = _configuration.GetSection("JwtSettings");
            var accessTokenExpiryMinutes = int.Parse(jwtSettings["ExpiryMinutes"] ?? "60");
            var refreshTokenExpiryDays = int.Parse(jwtSettings["RefreshTokenExpiryDays"] ?? "7");

            var accessToken = _jwtTokenService.GenerateAccessToken(claims);
            var refreshTokenValue = _jwtTokenService.GenerateRefreshToken();
            var accessTokenExpiresAt = DateTime.UtcNow.AddMinutes(accessTokenExpiryMinutes);
            var refreshTokenExpiresAt = DateTime.UtcNow.AddDays(refreshTokenExpiryDays);

            // Save refresh token
            var refreshToken = new RefreshToken
            {
                User = user,
                Token = refreshTokenValue,
                ExpiresAt = refreshTokenExpiresAt,
                IsRevoked = false,
                DeviceInfo = deviceInfo,
            };

            await _userRepository.AddRefreshTokenAsync(refreshToken);
            await _userRepository.UpdateAsync(user);
            await _unitOfWork.CommitTransactionAsync();
            _logger.LogInformation("Microsoft user {UserId} authenticated successfully", user.Id);

            return Result<AuthenticationResponseDto>.Success(new AuthenticationResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshTokenValue,
                AccessTokenExpiresAt = accessTokenExpiresAt,
                RefreshTokenExpiresAt = refreshTokenExpiresAt,
                User = MapToUserDto(user),
                Role = await GetRoleDtoAsync(user)
            });
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            _logger.LogError(ex, "Microsoft authentication failed for email {Email}", email);
            return _errorService.CreateFailure<AuthenticationResponseDto>(ErrorCodes.EXTERNAL_SERVICE_ERROR);
        }
    }

    public async Task<Result<AuthenticationResponseDto>> AuthenticateWithYahooAsync(string? email, string? firstName, string? lastName, string? picture, 
        string? externalProviderId, string? deviceInfo = null, CancellationToken cancellationToken = default)
    {
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            if (string.IsNullOrEmpty(email))
                return _errorService.CreateFailure<AuthenticationResponseDto>(ErrorCodes.VALIDATION_FAILED);

            var user = await _userRepository.GetByEmailAsync(email);

            if (user == null)
            {

                user = new User
                {
                    Id = Guid.NewGuid().ToString(),
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email.Split('@')[0],
                    PasswordHash = null, // External users have no pass
                    Status = UserStatus.Active, // verified users are active immediately
                    ExternalProvider = "Yahoo",
                    ExternalProviderId = externalProviderId,
                    EmailConfirmed = true,
                };


                await _userRepository.AddAsync(user);
                await _unitOfWork.SaveChangesAsync();

                //add automatiacaly user role
                string role = SystemPolicies.User;
                var roleResult = await _userManager.AddToRoleAsync(user, role);
                if (!roleResult.Succeeded)
                {
                    _logger.LogError("Failed to assign role {Role} to user {UserId}", role, user.Id);
                    return _errorService.CreateFailure<AuthenticationResponseDto>(ErrorCodes.VALIDATION_FAILED);
                }

                await _unitOfWork.SaveChangesAsync();
                _logger.LogInformation("New Yahoo user {UserId} created successfully", user.Id);
            }
            else
            {
                // Existing user - check if they can log in
                if (user.Status == UserStatus.Banned)
                    return _errorService.CreateFailure<AuthenticationResponseDto>(ErrorCodes.USER_BANNED);

                if (user.Status == UserStatus.Suspended)
                    return _errorService.CreateFailure<AuthenticationResponseDto>(ErrorCodes.USER_SUSPENDED);

                if (user.Status != UserStatus.Active)
                    return _errorService.CreateFailure<AuthenticationResponseDto>(ErrorCodes.USER_INACTIVE);
            }


            // Generate tokens
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Email, user.Email!),
                new(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new(ClaimTypes.Role, SystemPolicies.User.ToString()),
                new(ClaimNames.UserStatus, user.Status.ToString())
            };

            var jwtSettings = _configuration.GetSection("JwtSettings");
            var accessTokenExpiryMinutes = int.Parse(jwtSettings["ExpiryMinutes"] ?? "60");
            var refreshTokenExpiryDays = int.Parse(jwtSettings["RefreshTokenExpiryDays"] ?? "7");

            var accessToken = _jwtTokenService.GenerateAccessToken(claims);
            var refreshTokenValue = _jwtTokenService.GenerateRefreshToken();
            var accessTokenExpiresAt = DateTime.UtcNow.AddMinutes(accessTokenExpiryMinutes);
            var refreshTokenExpiresAt = DateTime.UtcNow.AddDays(refreshTokenExpiryDays);

            // Save refresh token
            var refreshToken = new RefreshToken
            {
                User = user,
                Token = refreshTokenValue,
                ExpiresAt = refreshTokenExpiresAt,
                IsRevoked = false,
                DeviceInfo = deviceInfo,
            };

            await _userRepository.AddRefreshTokenAsync(refreshToken);
            await _userRepository.UpdateAsync(user);
            await _unitOfWork.CommitTransactionAsync();
            _logger.LogInformation("Yahoo user {UserId} authenticated successfully", user.Id);

            return Result<AuthenticationResponseDto>.Success(new AuthenticationResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshTokenValue,
                AccessTokenExpiresAt = accessTokenExpiresAt,
                RefreshTokenExpiresAt = refreshTokenExpiresAt,
                User = MapToUserDto(user),
                Role = await GetRoleDtoAsync(user)
            });
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            _logger.LogError(ex, "Yahoo authentication failed for email {Email}", email);
            return _errorService.CreateFailure<AuthenticationResponseDto>(ErrorCodes.EXTERNAL_SERVICE_ERROR);
        }
    }


    public async Task<Result<AuthenticationResponseDto>> LoginAsync(string login, string password, string? ipAddress = null, string? deviceInfo = null)
    {
        try
        {
            var user = await _userRepository.GetByEmailAsync(login)
                       ?? await _userRepository.GetByUsernameAsync(login);
            if (user == null)
                return _errorService.CreateFailure<AuthenticationResponseDto>(ErrorCodes.USER_INVALID_CREDENTIALS);

            if (!_passwordService.VerifyPassword(password, user.PasswordHash!))
                return _errorService.CreateFailure<AuthenticationResponseDto>(ErrorCodes.USER_INVALID_CREDENTIALS);

            // Check user status - banned and suspended users cannot login
            if (user.Status == UserStatus.Banned)
                return _errorService.CreateFailure<AuthenticationResponseDto>(ErrorCodes.USER_BANNED);

            if (user.Status == UserStatus.Suspended)
                return _errorService.CreateFailure<AuthenticationResponseDto>(ErrorCodes.USER_SUSPENDED);

            if (user.Status != UserStatus.Active)
                return _errorService.CreateFailure<AuthenticationResponseDto>(ErrorCodes.USER_INACTIVE);

            var loginRoleNames = await _userManager.GetRolesAsync(user);
            var loginRoleName = loginRoleNames.FirstOrDefault();

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Email, user.Email!),
                new(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new(ClaimTypes.Role, loginRoleName ?? string.Empty),
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
                IPAddress = ipAddress,
                DeviceInfo = deviceInfo,
                CreatedOn = DateTime.UtcNow
            };

            await _userRepository.AddRefreshTokenAsync(refreshToken);

            await _userRepository.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("User {UserId} logged in successfully", user.Id);

            var response = new AuthenticationResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshTokenValue,
                AccessTokenExpiresAt = accessTokenExpiresAt,
                RefreshTokenExpiresAt = refreshTokenExpiresAt,
                User = MapToUserDto(user),
                Role = await GetRoleDtoAsync(user),
            };

            return Result<AuthenticationResponseDto>.Success(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Login failed for {Login}", login);
            return _errorService.CreateFailure<AuthenticationResponseDto>(ErrorCodes.EXTERNAL_SERVICE_ERROR);
        }
    }

    public async Task<Result<bool>> LogoutAsync(string userId)
    {
        try
        {
            // Revoke all refresh tokens for the user
            var userRefreshTokens = await _userRepository.GetRefreshTokensByUserIdAsync(userId);

            if (userRefreshTokens == null)
                return _errorService.CreateFailure<bool>(ErrorCodes.USER_NOT_FOUND);

            var activeTokens = userRefreshTokens.Where(t => !t.IsRevoked).ToList();
            if (activeTokens.Any())
            {
                foreach (var token in activeTokens)
                {
                    token.IsRevoked = true;
                    token.RevokedAt = DateTime.UtcNow;
                    token.RevokedBy = "User logout";
                }

                await _userRepository.UpdateRefreshTokensAsync(activeTokens);
                await _unitOfWork.SaveChangesAsync();
            }

            _logger.LogInformation("User {UserId} logged out successfully", userId);
            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Logout failed for user {UserId}", userId);
            return _errorService.CreateFailure<bool>(ErrorCodes.EXTERNAL_SERVICE_ERROR);
        }
    }



    public async Task<Result<bool>> ChangePasswordAsync(string userId, string currentPassword, string newPassword)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync<string>(userId);
            if (user == null)
                return _errorService.CreateFailure<bool>(ErrorCodes.NOT_FOUND);

            if (!_passwordService.VerifyPassword(currentPassword, user.PasswordHash!))
                return _errorService.CreateFailure<bool>(ErrorCodes.VALIDATION_FAILED);

            if (!_passwordService.IsPasswordStrong(newPassword))
                return _errorService.CreateFailure<bool>(ErrorCodes.VALIDATION_FAILED);

            // Check if new password is different from current
            if (_passwordService.VerifyPassword(newPassword, user.PasswordHash!))
                return _errorService.CreateFailure<bool>(ErrorCodes.VALIDATION_FAILED);

            user.PasswordHash = _passwordService.HashPassword(newPassword);
            await _userRepository.UpdateAsync(user);

            // Invalidate all refresh tokens for security
            var userRefreshTokens = await _userRepository.GetRefreshTokensByUserIdAsync(userId);
            foreach (var refreshToken in userRefreshTokens.Where(t => !t.IsRevoked))
            {
                refreshToken.IsRevoked = true;
                refreshToken.RevokedAt = DateTime.UtcNow;
                refreshToken.RevokedBy = "Password change";
            }

            await _userRepository.UpdateRefreshTokensAsync(userRefreshTokens);

            _logger.LogInformation("Password changed for user {UserId}", userId);
            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Password change failed for user {UserId}", userId);
            return _errorService.CreateFailure<bool>(ErrorCodes.EXTERNAL_SERVICE_ERROR);
        }
    }

    private async Task SaveProfileImageAsync(User user, string name, string data)
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

    private UserDto MapToUserDto(User user)
       => _mapper.Map<UserDto>(user);

    private async Task<RoleDto?> GetRoleDtoAsync(User user)
    {
        var names = await _userManager.GetRolesAsync(user);
        var name = names.FirstOrDefault();
        if (name == null) return null;
        var role = await _roleManager.FindByNameAsync(name);
        return role != null ? _mapper.Map<RoleDto>(role) : null;
    }
}
