using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;

namespace CarRentalZaimi.Application.Features.SavedCar.Queries;

public class GetAllSavedCarsQuery : IQuery<PagedResponse<SavedCarDto>>
{
    public string? UserId { get; set; }
    public string? CategoryId { get; set; }
    public string? Search { get; set; }
    public int PageNr { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
