using CarRentalZaimi.Application.Common;
using System.Text.Json.Serialization;

namespace CarRentalZaimi.Application.DTOs.ApiResponse;

public class ApiResponse
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public List<string> Errors { get; set; } = new();
    public List<ValidationError> ValidationErrors { get; set; } = new();

    [JsonIgnore]
    public Exception? Exception { get; set; }

    public static ApiResponse SuccessResponse(string? message = null)
        => new() { Success = true, Message = message };

    public static ApiResponse FailureResponse(string? error)
        => new() { Success = false, Errors = new List<string> { error ?? "An error occurred." } };

    public static ApiResponse FailureResponse(List<string> errors)
        => new() { Success = false, Errors = errors };

    public static ApiResponse FailureResponse(Exception exception, string message)
        => new() { Success = false, Errors = new List<string> { message }, Exception = exception };

    public static ApiResponse ValidationFailureResponse(List<ValidationError> validationErrors)
        => new()
        {
            Success = false,
            Errors = new List<string> { "Validation failed." },
            ValidationErrors = validationErrors
        };
}

public class ApiResponse<T> : ApiResponse
{
    public T? Data { get; set; }

    public static ApiResponse<T> SuccessResponse(T data, string? message = null)
        => new() { Success = true, Data = data, Message = message };

    public static new ApiResponse<T> FailureResponse(string? error)
        => new() { Success = false, Errors = new List<string> { error ?? "An error occurred." } };

    public static new ApiResponse<T> FailureResponse(List<string> errors)
        => new() { Success = false, Errors = errors };

    public static new ApiResponse<T> FailureResponse(Exception exception, string message)
        => new() { Success = false, Errors = new List<string> { message }, Exception = exception };

    public static new ApiResponse<T> ValidationFailureResponse(List<ValidationError> validationErrors)
        => new()
        {
            Success = false,
            Errors = new List<string> { "Validation failed." },
            ValidationErrors = validationErrors
        };
}
