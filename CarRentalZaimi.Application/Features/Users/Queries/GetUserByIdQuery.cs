using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;

namespace CarRentalZaimi.Application.Features.Users.Queries;

public class GetUserByIdQuery() : IQuery<UserDto>
{
    public string? UserId { get; set; }
}
