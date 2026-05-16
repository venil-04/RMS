using Microsoft.AspNetCore.Mvc;
using RMS.Infrastructure.Persistence;

namespace RMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DemoController(AppDbContext context) : BaseController
{
    [HttpGet("error")]
    public IActionResult ThrowError()
    {
        throw new Exception("Testing global exception middleware");
    }

    [HttpGet("success")]
    public IActionResult Success()
    {
        return OkResponse("API is working");
    }
}