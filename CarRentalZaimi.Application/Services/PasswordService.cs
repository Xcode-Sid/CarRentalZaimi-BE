using CarRentalZaimi.Application.Interfaces.Services;
using Microsoft.Extensions.Logging;
using CarRentalZaimi.Logging;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace CarRentalZaimi.Application.Services;


public class PasswordService(ILogger<PasswordService> _logger) : IPasswordService
{
    public string HashPassword(string password)
    {
        try
        {
            var salt = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
                rng.GetBytes(salt);

            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var saltedPassword = new byte[passwordBytes.Length + salt.Length];
            Buffer.BlockCopy(passwordBytes, 0, saltedPassword, 0, passwordBytes.Length);
            Buffer.BlockCopy(salt, 0, saltedPassword, passwordBytes.Length, salt.Length);

            using var sha256 = SHA256.Create();
            var hash = sha256.ComputeHash(saltedPassword);

            var result = new byte[salt.Length + hash.Length];
            Buffer.BlockCopy(salt, 0, result, 0, salt.Length);
            Buffer.BlockCopy(hash, 0, result, salt.Length, hash.Length);

            return Convert.ToBase64String(result);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Failed to hash password");
            throw;
        }
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        try
        {
            var storedBytes = Convert.FromBase64String(hashedPassword);

            var salt = new byte[32];
            Buffer.BlockCopy(storedBytes, 0, salt, 0, 32);

            var storedHash = new byte[storedBytes.Length - 32];
            Buffer.BlockCopy(storedBytes, 32, storedHash, 0, storedHash.Length);

            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var saltedPassword = new byte[passwordBytes.Length + salt.Length];
            Buffer.BlockCopy(passwordBytes, 0, saltedPassword, 0, passwordBytes.Length);
            Buffer.BlockCopy(salt, 0, saltedPassword, passwordBytes.Length, salt.Length);

            using var sha256 = SHA256.Create();
            var computedHash = sha256.ComputeHash(saltedPassword);

            return computedHash.SequenceEqual(storedHash);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Failed to verify password");
            return false;
        }
    }

    public bool IsPasswordStrong(string password)
    {
        if (string.IsNullOrEmpty(password) || password.Length < 8)
            return false;

        if (!Regex.IsMatch(password, @"[a-z]"))
            return false;

        if (!Regex.IsMatch(password, @"[A-Z]"))
            return false;

        if (!Regex.IsMatch(password, @"[0-9]"))
            return false;

        if (!Regex.IsMatch(password, @"[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]"))
            return false;

        return true;
    }

    public string GenerateRandomPassword(int length = 12)
    {
        const string lowercase = "abcdefghijklmnopqrstuvwxyz";
        const string uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string digits = "0123456789";
        const string special = "!@#$%^&*()_+-=[]{}|;:,.<>?";

        var allChars = lowercase + uppercase + digits + special;
        var random = new Random();
        var password = new StringBuilder();

        password.Append(lowercase[random.Next(lowercase.Length)]);
        password.Append(uppercase[random.Next(uppercase.Length)]);
        password.Append(digits[random.Next(digits.Length)]);
        password.Append(special[random.Next(special.Length)]);

        for (int i = 4; i < length; i++)
        {
            password.Append(allChars[random.Next(allChars.Length)]);
        }

        var passwordArray = password.ToString().ToCharArray();
        for (int i = passwordArray.Length - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            (passwordArray[i], passwordArray[j]) = (passwordArray[j], passwordArray[i]);
        }

        return new string(passwordArray);
    }
}

