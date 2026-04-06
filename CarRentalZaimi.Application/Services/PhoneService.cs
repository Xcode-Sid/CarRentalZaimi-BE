using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Common.Errors;
using CarRentalZaimi.Application.Common.Phone;
using CarRentalZaimi.Application.Interfaces.Services;
using CarRentalZaimi.Application.Interfaces.UnitOfWork;
using CarRentalZaimi.Domain.Entities;
using CarRentalZaimi.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace CarRentalZaimi.Application.Services;

public class PhoneService(
    ILogger<PhoneService> _logger,
    IUnitOfWork _unitOfWork,
    IErrorService _errorService,
    IOptions<PhoneSettings> _phoneSettings) : IPhoneService
{
    private readonly PhoneSettings _settings = _phoneSettings.Value;
    public async Task<Result<bool>> SendVerificationCodeAsync(
        string userId,
        CancellationToken cancellationToken = default)
    {
        var user = await _unitOfWork.Repository<User>()
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

        if (user is null)
            return Result<bool>.Error("User not found");

        if (string.IsNullOrWhiteSpace(user.PhoneNumber))
            return Result<bool>.Error("User does not have a phone number");

        var existingTokens = await _unitOfWork.Repository<PhoneConfirmationToken>()
            .AsQueryable()
            .Where(t => t.User!.Id == userId && !t.IsUsed && t.ExpiresAt > DateTime.UtcNow)
            .ToListAsync(cancellationToken);

        foreach (var old in existingTokens)
        {
            old.IsUsed = true;
            old.UsedAt = DateTime.UtcNow;
            await _unitOfWork.Repository<PhoneConfirmationToken>()
                .UpdateAsync(old, cancellationToken);
        }

        var code = GenerateCode();

        var token = new PhoneConfirmationToken
        {
            Id = Guid.NewGuid(),
            Code = code,
            ExpiresAt = DateTime.UtcNow.AddMinutes(_settings.CodeExpiryMinutes),
            IsUsed = false,
            User = user,
        };

        await _unitOfWork.Repository<PhoneConfirmationToken>()
            .AddAsync(token, cancellationToken);

        var message = $"Your {_settings.ServiceName} verification code is: {code}. " +
              $"This code expires in {_settings.CodeExpiryMinutes} minutes.";

        var smsSent = await SendSmsAsync(user.PhoneNumber, code, cancellationToken);
        if (!smsSent.IsSuccessful)
        {
            return Result<bool>.Error("Failed to send SMS. Please try again.");
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> ConfirmPhoneAsync(string userId, string code, CancellationToken cancellationToken = default)
    {
        try
        {
            var user = await _unitOfWork.Repository<User>()
                .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

            if (user is null)
                return _errorService.CreateFailure<bool>(ErrorCodes.NOT_FOUND);

            if (user.PhoneNumberConfirmed)
                return Result<bool>.Success(true);

            var confirmationToken = await _unitOfWork.Repository<PhoneConfirmationToken>()
                .AsQueryable()
                .Include(t => t.User)
                .Where(t => t.User != null && t.User.Id == userId && !t.IsUsed)
                .OrderByDescending(t => t.CreatedOn)
                .FirstOrDefaultAsync(cancellationToken);

            if (confirmationToken is null)
                return _errorService.CreateFailure<bool>(ErrorCodes.INVALID_TOKEN);

            if (confirmationToken.Code != code)
                return _errorService.CreateFailure<bool>(ErrorCodes.INVALID_TOKEN);

            if (confirmationToken.ExpiresAt < DateTime.UtcNow)
                return _errorService.CreateFailure<bool>(ErrorCodes.TOKEN_EXPIRED);

            confirmationToken.IsUsed = true;
            confirmationToken.UsedAt = DateTime.UtcNow;
            await _unitOfWork.Repository<PhoneConfirmationToken>()
                .UpdateAsync(confirmationToken, cancellationToken);

            user.PhoneNumberConfirmed = true;

            if (user.Status == UserStatus.PendingVerification)
                user.Status = UserStatus.Active;

            await _unitOfWork.Repository<User>().UpdateAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation(
                "Phone confirmed for user {UserId}, status set to {Status}",
                userId, user.Status);

            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to confirm phone for user {UserId}", userId);
            return _errorService.CreateFailure<bool>(ErrorCodes.EXTERNAL_SERVICE_ERROR);
        }
    }


    private static string GenerateCode()
    {
        var code = RandomNumberGenerator.GetInt32(0, 1_000_000);
        return code.ToString("D6"); // zero-padded, always 6 digits
    }


    public async Task<Result<bool>> SendSmsAsync(string phoneNumber, string message, CancellationToken cancellationToken = default)
    {
        try
        {
            if (!_settings.EnableSms)
            {
                _logger.LogWarning("SMS sending is disabled");
                return Result<bool>.Success(true);
            }

            if (string.IsNullOrEmpty(_settings.TwilioAccountSid) || string.IsNullOrEmpty(_settings.TwilioAuthToken))
            {
                _logger.LogError("Invalid Twilio credentials");
                return _errorService.CreateFailure<bool>(ErrorCodes.EXTERNAL_SERVICE_ERROR);
            }

            // Validate and format phone number using country code database
            var validatedPhoneNumber = await ValidateAndFormatPhoneNumberAsync(phoneNumber, cancellationToken);
            if (string.IsNullOrEmpty(validatedPhoneNumber))
            {
                _logger.LogWarning("Invalid phone number format: {PhoneNumber}", phoneNumber);
                return _errorService.CreateFailure<bool>(ErrorCodes.VALIDATION_FAILED);
            }

            // Validate message length
            if (message.Length > _settings.MaxMessageLength)
                return _errorService.CreateFailure<bool>(ErrorCodes.VALIDATION_FAILED);

            // Validate Twilio phone number is configured
            if (string.IsNullOrEmpty(_settings.TwilioPhoneNumber))
            {
                _logger.LogError("Twilio phone number not configured");
                return _errorService.CreateFailure<bool>(ErrorCodes.EXTERNAL_SERVICE_ERROR);
            }

            // Initialize Twilio client
            TwilioClient.Init(_settings.TwilioAccountSid, _settings.TwilioAuthToken);

            // Send SMS using Twilio (Note: Twilio API version "2010" is just their API path structure, not a year)
            var messageResource = await MessageResource.CreateAsync(
                to: new PhoneNumber(validatedPhoneNumber),
                from: new PhoneNumber(_settings.TwilioPhoneNumber),
                body: message
            );

            if (messageResource.ErrorCode.HasValue)
            {
                _logger.LogError("Twilio error {ErrorCode}: {ErrorMessage} when sending SMS to {PhoneNumber}",
                    messageResource.ErrorCode, messageResource.ErrorMessage, validatedPhoneNumber);
                return _errorService.CreateFailure<bool>(ErrorCodes.EXTERNAL_SERVICE_ERROR);
            }

            _logger.LogInformation("SMS sent successfully to {PhoneNumber}. Message SID: {MessageSid}, Status: {Status}",
                validatedPhoneNumber, messageResource.Sid, messageResource.Status);

            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send SMS to {PhoneNumber}", phoneNumber);
            return _errorService.CreateFailure<bool>(ErrorCodes.EXTERNAL_SERVICE_ERROR);
        }
    }

    private async Task<string?> ValidateAndFormatPhoneNumberAsync(
        string phoneNumber,
        CancellationToken cancellationToken)
    {
        // Look up the matching prefix from your StatePrefix table
        var prefixes = await _unitOfWork.Repository<StatePrefix>()
            .AsQueryable()
            .ToListAsync(cancellationToken);

        foreach (var prefix in prefixes)
        {
            if (string.IsNullOrWhiteSpace(prefix.PhoneRegex))
                continue;

            var fullNumber = phoneNumber.StartsWith('+')
                ? phoneNumber
                : $"{prefix.PhonePrefix}{phoneNumber}";

            if (Regex.IsMatch(fullNumber, prefix.PhoneRegex))
                return fullNumber;
        }

        return null;
    }

    
}
