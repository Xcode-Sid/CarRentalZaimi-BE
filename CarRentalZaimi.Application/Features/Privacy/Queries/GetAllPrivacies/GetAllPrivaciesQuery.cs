using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;

namespace CarRentalZaimi.Application.Features.Privacy.Queries.GetAllPrivacies;

public class GetAllPrivaciesQuery() : IQuery<IEnumerable<PrivacyDto>>;
