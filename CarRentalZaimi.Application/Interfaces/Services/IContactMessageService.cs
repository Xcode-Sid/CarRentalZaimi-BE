using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.ContactMessage.Commands.CreateContactMessage;

namespace CarRentalZaimi.Application.Interfaces.Services;

public interface IContactMessageService
{
    Task<Result<bool>> CreateAsync(CreateContactMessageCommand request, CancellationToken cancellationToken = default);
}
