using CarRentalZaimi.Application.Common.Email;
using CarRentalZaimi.Application.Common.Errors;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Interfaces.Services;
using CarRentalZaimi.Domain.Entities;
using CarRentalZaimi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;
using CarRentalZaimi.Logging;

namespace CarRentalZaimi.Infrastructure.Identity;

public class PasswordResetService(
    ApplicationDbContext _context,
    IEmailService _emailService,
    IPasswordService _passwordService,
    ILogger<PasswordResetService> _logger,
    IOptions<EmailSettings> _emailSettings,
    IErrorService _errorService) : IPasswordResetService
{
    public async Task<ApiResponse<string>> GeneratePasswordResetTokenAsync(string email)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
                return _errorService.CreateFailure<string>(ErrorCodes.USER_NOT_FOUND);

            var token = GenerateSecureToken();
            var tokenHash = HashToken(token);

            var existingTokens = await _context.PasswordResetTokens
                .Where(t => t.User != null && t.User.Id == user.Id && !t.IsUsed)
                .ToListAsync();

            foreach (var existingToken in existingTokens)
            {
                existingToken.IsUsed = true;
                existingToken.UsedAt = DateTime.UtcNow;
            }

            var resetToken = new PasswordResetToken
            {
                Id = Guid.NewGuid(),
                User = user,
                TokenHash = tokenHash,
                ExpiresAt = DateTime.UtcNow.AddHours(1),
                CreatedAt = DateTime.UtcNow,
                IsUsed = false
            };

            _context.PasswordResetTokens.Add(resetToken);
            await _context.SaveChangesAsync();

            var baseUrl = _emailSettings.Value.BaseUrl;
            if (!Uri.TryCreate(baseUrl, UriKind.Absolute, out var parsedBaseUrl))
            {
                _logger.Error("Email BaseUrl is missing or invalid in configuration.");
                return _errorService.CreateFailure<string>(ErrorCodes.EXTERNAL_SERVICE_ERROR);
            }

            var resetLink = $"{parsedBaseUrl}reset-password?token={token}&email={email}";

            var emailData = new Dictionary<string, object>
            {
                { "FirstName", user.FirstName! },
                { "ResetLink", resetLink },
                { "ExpirationHours", 1 }
            };

            var emailResult = await _emailService.SendForgotPasswordEmailAsync(email, user!.FirstName!, resetLink);

            if (!emailResult.IsSuccess)
            {
                _logger.Error("Failed to send password reset email to {Email}", email);
                return _errorService.CreateFailure<string>(ErrorCodes.EXTERNAL_SERVICE_ERROR);
            }

            _logger.Info("Password reset token generated and email sent to {Email}", email);
            return ApiResponse<string>.SuccessResponse("Password reset email sent if account exists");
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Failed to generate password reset token for {Email}", email);
            return _errorService.CreateFailure<string>(ErrorCodes.EXTERNAL_SERVICE_ERROR);
        }
    }

    public async Task<ApiResponse<bool>> ResetPasswordAsync(string token, string email, string newPassword)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && !u.IsDeleted);
            if (user == null)
                return _errorService.CreateFailure<bool>(ErrorCodes.NOT_FOUND);

            if (!_passwordService.IsPasswordStrong(newPassword))
                return _errorService.CreateFailure<bool>(ErrorCodes.VALIDATION_FAILED);

            var tokenHash = HashToken(token);
            var resetToken = await _context.PasswordResetTokens
                .FirstOrDefaultAsync(t => t.User != null && t.User.Id == user.Id && t.TokenHash == tokenHash && !t.IsUsed);

            if (resetToken == null)
                return _errorService.CreateFailure<bool>(ErrorCodes.VALIDATION_FAILED);

            if (resetToken.ExpiresAt < DateTime.UtcNow)
                return _errorService.CreateFailure<bool>(ErrorCodes.VALIDATION_FAILED);

            resetToken.IsUsed = true;
            resetToken.UsedAt = DateTime.UtcNow;

            user.PasswordHash = _passwordService.HashPassword(newPassword);

            var userRefreshTokens = await _context.RefreshTokens
                .Where(t => t.User != null && t.User.Id == user.Id && !t.IsRevoked)
                .ToListAsync();

            foreach (var refreshToken in userRefreshTokens)
            {
                refreshToken.IsRevoked = true;
                refreshToken.RevokedAt = DateTime.UtcNow;
                refreshToken.RevokedBy = "Password Reset";
            }

            await _context.SaveChangesAsync();

            _logger.Info("Password reset completed for user {UserId}", user.Id);
            return ApiResponse<bool>.SuccessResponse(true);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Failed to reset password for email {Email}", email);
            return _errorService.CreateFailure<bool>(ErrorCodes.EXTERNAL_SERVICE_ERROR);
        }
    }

    private string GenerateSecureToken()
    {
        var randomBytes = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes).Replace("+", "-").Replace("/", "_").Replace("=", "");
    }

    private string HashToken(string token)
    {
        using var sha256 = SHA256.Create();
        var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(token));
        return Convert.ToBase64String(hashBytes);
    }
}

