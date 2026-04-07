using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.CarCompanyName.Commands.DeleteCarCompanyName;

internal class DeleteCarCompanyNameCommandHandler(ICarCompanyNameService _carCompanyNameService) : ICommandHandler<DeleteCarCompanyNameCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteCarCompanyNameCommand request, CancellationToken cancellationToken)
        => await _carCompanyNameService.DeleteAsync(request, cancellationToken);
}

