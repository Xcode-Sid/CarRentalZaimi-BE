using CarRentalZaimi.Application.Common;
using MediatR;

namespace CarRentalZaimi.Application.Interfaces.Query;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}
