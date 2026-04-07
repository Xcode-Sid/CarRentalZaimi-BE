using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Interfaces.Command;
using Microsoft.Extensions.Logging;
using CarRentalZaimi.Logging;

namespace CarRentalZaimi.Application.Features.Cars.Commands.RegisterCar;

public class RegisterCarCommandHandler(
    ILogger<RegisterCarCommandHandler> _logger) : ICommandHandler<RegisterCarCommand, CarDto>
{
    public Task<ApiResponse<CarDto>> Handle(RegisterCarCommand request, CancellationToken cancellationToken)
    {
        _logger.Info("Handling RegisterCarCommand for {Make} {Model} ({Year})",
            request.Make, request.Model, request.Year);

        throw new NotImplementedException();
    }
}
