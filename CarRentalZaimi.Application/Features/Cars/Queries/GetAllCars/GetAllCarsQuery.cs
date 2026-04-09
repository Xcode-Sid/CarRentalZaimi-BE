using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;

namespace CarRentalZaimi.Application.Features.Cars.Queries.GetAllCars;


public class GetAllCarsQuery : IQuery<IEnumerable<CarDto>>;

