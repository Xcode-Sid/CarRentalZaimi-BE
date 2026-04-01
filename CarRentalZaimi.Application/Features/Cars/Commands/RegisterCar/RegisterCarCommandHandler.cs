using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;
using Microsoft.Extensions.Logging;

namespace CarRentalZaimi.Application.Features.Cars.Commands.RegisterCar;

public class RegisterCarCommandHandler(
    ILogger<RegisterCarCommandHandler> _logger) : ICommandHandler<RegisterCarCommand, CarDto>
{
    public Task<Result<CarDto>> Handle(RegisterCarCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling RegisterCarCommand for {Make} {Model} ({Year})",
            request.Make, request.Model, request.Year);

        throw new NotImplementedException();
    }
}
