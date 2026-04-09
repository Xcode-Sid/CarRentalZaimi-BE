using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;

namespace CarRentalZaimi.Application.Features.Cars.Queries.GetAllCars;

public record GetAllPagedCarsQuery : IQuery<PagedResponse<CarDto>>
{
    public string? Search { get; set; }
    public int PageNr { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public string? CategoryId { get; set; }
    public string? FuelTypeId { get; set; }
    public string? TransmissionId { get; set; }
    public int? Seats { get; set; }
    public decimal? PriceMin { get; set; }
    public decimal? PriceMax { get; set; }

    // Sort
    public string? SortBy { get; set; }

    public string? UserId { get; set; }
}
