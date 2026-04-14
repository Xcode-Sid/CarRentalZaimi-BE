using AutoMapper;
using CarRentalZaimi.Application.DTOs.ApiResponse;

namespace CarRentalZaimi.Application.Helpers;
public class ApiResponseConverter<TSource, TDest>
    : ITypeConverter<ApiResponse<List<TSource>>, ApiResponse<List<TDest>>>
{
    public ApiResponse<List<TDest>> Convert(
        ApiResponse<List<TSource>> source,
        ApiResponse<List<TDest>> destination,
        ResolutionContext context)
    {
        return new ApiResponse<List<TDest>>
        {
            IsSuccess = source.IsSuccess,
            Message = source.Message,
            Errors = source.Errors,
            ValidationErrors = source.ValidationErrors,
            Exception = source.Exception,
            Data = source.Data != null
                ? context.Mapper.Map<List<TDest>>(source.Data)
                : null
        };
    }
}