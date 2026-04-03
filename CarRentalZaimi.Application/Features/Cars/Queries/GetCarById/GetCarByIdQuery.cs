using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;

namespace CarRentalZaimi.Application.Features.Cars.Queries.GetCarById;

public class GetCarByIdQuery(string? id) : IQuery<CarDto>;
