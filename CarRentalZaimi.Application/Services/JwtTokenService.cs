using CarRentalZaimi.Application.Common.Constants;
using CarRentalZaimi.Application.Common.Errors;
using CarRentalZaimi.Application.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CarRentalZaimi.Application.Services;

public class JwtTokenService(IConfiguration _configuration, ILogger<JwtTokenService> _logger) : IJwtTokenService
{
    public string GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var jwtSettings = _configuration.GetSection(ConfigurationKeys.Sections.JwtSettings);
        var secretKey = jwtSettings[ConfigurationKeys.JwtSettingKeys.SecretKey] ?? throw new InvalidOperationException(ExceptionMessages.JwtSecretKeyNotConfigured);
        var issuer = jwtSettings[ConfigurationKeys.JwtSettingKeys.Issuer] ?? "SquadBuddy";
        var audience = jwtSettings[ConfigurationKeys.JwtSettingKeys.Audience] ?? "SquadBuddy";
        var expiryMinutes = int.Parse(jwtSettings[ConfigurationKeys.JwtSettingKeys.ExpiryMinutes] ?? "60");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
    {
        var jwtSettings = _configuration.GetSection(ConfigurationKeys.Sections.JwtSettings);
        var secretKey = jwtSettings[ConfigurationKeys.JwtSettingKeys.SecretKey] ?? throw new InvalidOperationException(ExceptionMessages.JwtSecretKeyNotConfigured);
        var issuer = jwtSettings[ConfigurationKeys.JwtSettingKeys.Issuer] ?? "SquadBuddy";
        var audience = jwtSettings[ConfigurationKeys.JwtSettingKeys.Audience] ?? "SquadBuddy";

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            ValidIssuer = issuer,
            ValidAudience = audience,
            ValidateLifetime = false,
            ClockSkew = TimeSpan.Zero
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException(ExceptionMessages.InvalidToken);

        return principal;
    }

    public bool ValidateToken(string token)
    {
        try
        {
            var jwtSettings = _configuration.GetSection(ConfigurationKeys.Sections.JwtSettings);
            var secretKey = jwtSettings[ConfigurationKeys.JwtSettingKeys.SecretKey] ?? throw new InvalidOperationException(ExceptionMessages.JwtSecretKeyNotConfigured);
            var issuer = jwtSettings[ConfigurationKeys.JwtSettingKeys.Issuer] ?? "SquadBuddy";
            var audience = jwtSettings[ConfigurationKeys.JwtSettingKeys.Audience] ?? "SquadBuddy";

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                ValidIssuer = issuer,
                ValidAudience = audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Token validation failed");
            return false;
        }
    }

    public DateTime GetTokenExpiration(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            return jwtToken.ValidTo;
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to get token expiration");
            return DateTime.MinValue;
        }
    }
}

