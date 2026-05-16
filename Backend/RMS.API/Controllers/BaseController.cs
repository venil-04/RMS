using Microsoft.AspNetCore.Mvc;
using RMS.Application.Common.Responses;

namespace RMS.API.Controllers;

[ApiController]
public abstract class BaseController : ControllerBase
{
    protected IActionResult OkResponse<T>(T data, string message = "Success")
    {
        var response = ApiResponse<T>.Ok(data, message);
        return Ok(response);
    }

    protected IActionResult BadRequestResponse(string message, List<string>? errors = null)
    {
        var response = ApiResponse<string>.Fail(message, errors);
        return BadRequest(response);
    }

    protected IActionResult NotFoundResponse(string message = "Data not found")
    {
        var response = ApiResponse<string>.Fail(message);
        return NotFound(response);
    }
}