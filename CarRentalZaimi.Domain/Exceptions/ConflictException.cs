using System.Net;

namespace CarRentalZaimi.Domain.Exceptions;

public sealed class ConflictException(string message)
    : AppException(message, HttpStatusCode.Conflict);
