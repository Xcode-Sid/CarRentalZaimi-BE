using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;

namespace CarRentalZaimi.Application.Features.CarTransmission.Queries.GetAllCarFuels;

public class GetAllCarTransmissionQuery() : IQuery<IEnumerable<CarTransmissionDto>>;
