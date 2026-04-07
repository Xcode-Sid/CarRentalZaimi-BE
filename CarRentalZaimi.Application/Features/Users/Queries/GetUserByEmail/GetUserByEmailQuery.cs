using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;

namespace CarRentalZaimi.Application.Features.Users.Queries.GetUserByEmail;

public class GetUserByEmailQuery() : IQuery<UserDto>
{
    public string? Email { get; set; }
}
