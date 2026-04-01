using CarRentalZaimi.Application.Common;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalZaimi.API.Controllers.Base;

[ApiController]
[Route("api/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    protected IActionResult FromResult(Result result, int successStatusCode = 200)
    {
        if (!result.IsSuccessful)
        {
            if (result.ValidationErrors.HasErrors)
                return BadRequest(result);

            return StatusCode(GetErrorStatusCode(result), result);
        }

        return StatusCode(successStatusCode, result);
    }

    protected IActionResult FromResult<T>(Result<T> result, int successStatusCode = 200)
    {
        if (!result.IsSuccessful)
        {
            if (result.ValidationErrors.HasErrors)
                return BadRequest(result);

            return StatusCode(GetErrorStatusCode(result), result);
        }

        return StatusCode(successStatusCode, result);
    }

    private static int GetErrorStatusCode<T>(Result<T> result)
    {
        if (result.Exception is Domain.Exceptions.AppException appEx)
            return (int)appEx.StatusCode;

        return 500;
    }

    private static int GetErrorStatusCode(Result result)
    {
        if (result.Exception is Domain.Exceptions.AppException appEx)
            return (int)appEx.StatusCode;

        return 500;
    }
}
