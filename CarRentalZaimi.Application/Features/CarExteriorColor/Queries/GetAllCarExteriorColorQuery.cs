using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;

namespace CarRentalZaimi.Application.Features.CarExteriorColor.Queries;

public class GetAllCarExteriorColorQuery() : IQuery<IEnumerable<CarExteriorColorDto>>;