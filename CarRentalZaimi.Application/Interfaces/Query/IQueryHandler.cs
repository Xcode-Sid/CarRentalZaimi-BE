using CarRentalZaimi.Application.DTOs.ApiResponse;
using MediatR;

namespace CarRentalZaimi.Application.Interfaces.Query;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, ApiResponse<TResponse>>
    where TQuery : IQuery<TResponse>
{
}
