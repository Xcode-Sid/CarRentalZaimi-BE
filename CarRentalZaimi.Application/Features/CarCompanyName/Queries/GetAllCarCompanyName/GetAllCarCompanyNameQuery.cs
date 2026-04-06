using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;

namespace CarRentalZaimi.Application.Features.CarCompanyName.Queries.GetAllCarCompanyName;

public class GetAllCarCompanyNameQuery() : IQuery<IEnumerable<CarCompanyNameDto>>;
