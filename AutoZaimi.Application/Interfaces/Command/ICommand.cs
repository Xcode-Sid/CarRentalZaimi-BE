using MediatR;

namespace CarRentalZaimi.Application.Interfaces.Command;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}


public interface ICommandHandler<TCommand, TResponse>
    : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
    where TResponse : class
{
}
