using CollabSphere.API.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CollabSphere.API.Controllers;

[ApiController]
public abstract class ApiControllerBase : ControllerBase
{
    protected IActionResult Success<T>(T data, string message = "Thành công")
    {
        return Ok(ApiResponse<T>.SuccessResult(data, message));
    }

    protected IActionResult CreatedOk<T>(T data, string message = "Tạo thành công")
    {
        return StatusCode(201, ApiResponse<T>.SuccessResult(data, message));
    }

    protected IActionResult Failure(int statusCode, string message)
    {
        return StatusCode(statusCode, ApiErrorResponse.From(statusCode, message));
    }
}
