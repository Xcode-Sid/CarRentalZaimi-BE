using CarRentalZaimi.Application.DTOs.ApiResponse;

namespace CarRentalZaimi.Application.Interfaces.Services;

public interface IErrorService
{
    ApiResponse<T> CreateFailure<T>(string errorCode, string? language = null, params object[] parameters);

    ApiResponse<T> CreateFailure<T>(string errorCode, Exception? exception, string? language = null, params object[] parameters);

    ApiResponse CreateFailure(string errorCode, string? language = null, params object[] parameters);

    ApiResponse CreateFailure(string errorCode, Exception? exception, string? language = null, params object[] parameters);

    string GetErrorMessage(string errorCode, string? language = null, params object[] parameters);

    string GetCurrentUserLanguage();
}
