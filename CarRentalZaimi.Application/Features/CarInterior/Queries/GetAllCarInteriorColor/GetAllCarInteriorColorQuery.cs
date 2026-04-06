using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;

namespace CarRentalZaimi.Application.Features.CarInterior.Queries.GetAllCarInteriorColor;

public class GetAllCarInteriorColorQuery() : IQuery<IEnumerable<CarInteriorColorDto>>;