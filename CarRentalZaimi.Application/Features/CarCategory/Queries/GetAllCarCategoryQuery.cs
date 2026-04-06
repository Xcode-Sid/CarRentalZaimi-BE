using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;

namespace CarRentalZaimi.Application.Features.CarCategory.Queries;

public class GetAllCarCategoryQuery() : IQuery<IEnumerable<CarCategoryDto>>;
