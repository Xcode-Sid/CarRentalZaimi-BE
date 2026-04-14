using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.ContactMessage.Commands.CreateContactMessage;

internal class CreateContactMessageCommandHandler(IContactMessageService _contactMessageService) : ICommandHandler<CreateContactMessageCommand, bool>
{
    public async Task<Result<bool>> Handle(CreateContactMessageCommand request, CancellationToken cancellationToken)
        => await _contactMessageService.CreateAsync(request, cancellationToken);
}
