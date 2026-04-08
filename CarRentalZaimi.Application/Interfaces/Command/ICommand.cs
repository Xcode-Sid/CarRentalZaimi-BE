using CarRentalZaimi.Application.DTOs.ApiResponse;
using MediatR;

namespace CarRentalZaimi.Application.Interfaces.Command;

public interface ICommand<TData> : IRequest<ApiResponse<TData>>
{
}

public interface ICommandHandler<TCommand, TData>
    : IRequestHandler<TCommand, ApiResponse<TData>>
    where TCommand : ICommand<TData>
{
}
