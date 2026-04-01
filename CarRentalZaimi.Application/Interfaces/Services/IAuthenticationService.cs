using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CarRentalZaimi.Application.Interfaces.Services;

public interface IAuthenticationService
{
    Task<Result<UserDto>> RegisterAsync(string firstname, string lastname, DateTime? dateOfBirth, string username, string email, string phone, string password, string? name, string? data, string? deviceInfo = null);
}
