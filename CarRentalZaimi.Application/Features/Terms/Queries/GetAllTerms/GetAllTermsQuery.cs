using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;

namespace CarRentalZaimi.Application.Features.Terms.Queries.GetAllTerms;

public class GetAllTermsQuery() : IQuery<IEnumerable<TermsDto>>;
