using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;

namespace CarRentalZaimi.Application.Features.CarCompanyModel.Queries.GetAllCarCompanyModel;

public class GetAllCarCompanyModelQuery() : IQuery<IEnumerable<CarCompanyModelDto>>;
