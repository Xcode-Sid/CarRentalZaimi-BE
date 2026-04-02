using CarRentalZaimi.Application.Common;
using MediatR;

namespace CarRentalZaimi.Application.Interfaces.Query;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}

