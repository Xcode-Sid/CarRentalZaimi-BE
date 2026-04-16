using AutoMapper;
using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Features.ContactMessage.Commands.CreateContactMessage;
using CarRentalZaimi.Application.Interfaces.Services;
using CarRentalZaimi.Application.Interfaces.UnitOfWork;
using CarRentalZaimi.Domain.Common.Constants;
using CarRentalZaimi.Domain.Entities;
using CarRentalZaimi.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace CarRentalZaimi.Application.Services;

public class ContactMessageService(IUnitOfWork _unitOfWork, IMapper _mapper, IEmailService _emailService, UserManager<User> _userManager, INotificationService _notificationService) : IContactMessageService
{
    public async Task<Result<bool>> CreateAsync(CreateContactMessageCommand request, CancellationToken cancellationToken = default)
    {
        var newMessage = new ContactMessage
        {
            FullName = request.FullName,
            Email = request.Email,
            Phone = request.Phone,
            Subject = request.Subject,
            Message = request.Message
        };

        await _unitOfWork.Repository<ContactMessage>().AddAsync(newMessage, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Notify admin
        await _notificationService.SendNotificationToAdminsAsync(
            $"{request.FullName} has sent a message with subject {request.Subject}.",
            UserNotificationType.NewContactMessage);

        var adminUsers = await _userManager.GetUsersInRoleAsync(SystemPolicies.Admin);
        var admin = adminUsers.FirstOrDefault();

        if (admin is not null)
            await _emailService.SendContactMessageAdminNotificationAsync(
                fullName: newMessage.FullName!,
                email: newMessage.Email!,
                phone: newMessage.Phone ?? "N/A",
                subject: newMessage.Subject!,
                message: newMessage.Message!,
                adminEmail: admin.Email!,
                cancellationToken: cancellationToken
                );

        return Result<bool>.Success(_mapper.Map<bool>(true));
    }
}
