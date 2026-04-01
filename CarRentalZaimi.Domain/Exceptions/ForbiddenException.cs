using System.Net;

namespace CarRentalZaimi.Domain.Exceptions;

public sealed class ForbiddenException(string message = "You do not have permission to perform this action.")
    : AppException(message, HttpStatusCode.Forbidden);
