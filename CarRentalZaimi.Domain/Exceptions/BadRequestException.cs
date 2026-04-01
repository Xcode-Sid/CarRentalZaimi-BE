using System.Net;

namespace CarRentalZaimi.Domain.Exceptions;

public sealed class BadRequestException(string message)
    : AppException(message, HttpStatusCode.BadRequest);
