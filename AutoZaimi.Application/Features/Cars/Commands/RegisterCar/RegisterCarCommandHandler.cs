using AutoZaimi.Application.DTOs;
using AutoZaimi.Application.Interfaces.Command;
using MediatR;

namespace AutoZaimi.Application.Features.Cars.Commands.RegisterCar;

public class RegisterCarCommandHandler : ICommandHandler<RegisterCarCommand, CarDto>
{
    public Task<CarDto> Handle(RegisterCarCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
