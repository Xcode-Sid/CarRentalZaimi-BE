using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;

namespace CarRentalZaimi.Application.Features.CarFuel.Queries.GetAllCarFuels;

public class GetAllCarFuelsQuery() : IQuery<IEnumerable<CarFuelDto>>;
