using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;

namespace CarRentalZaimi.Application.Features.StatePrefixes.Queries;

public class GetAllStatePrefixesQuery() : IQuery<IEnumerable<StatePrefixDto>>;