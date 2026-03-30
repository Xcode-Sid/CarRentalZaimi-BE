using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;
using MediatR;

namespace CarRentalZaimi.Application.Features.Cars.Commands.RegisterCar;

public class RegisterCarCommandHandler : ICommandHandler<RegisterCarCommand, CarDto>
{
    public Task<CarDto> Handle(RegisterCarCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
