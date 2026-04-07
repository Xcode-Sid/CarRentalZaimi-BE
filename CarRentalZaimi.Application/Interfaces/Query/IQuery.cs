using CarRentalZaimi.Application.DTOs.ApiResponse;
using MediatR;

namespace CarRentalZaimi.Application.Interfaces.Query;

public interface IQuery<TResponse> : IRequest<ApiResponse<TResponse>>
{
}
