using Microsoft.AspNetCore.Mvc;
using RMS.Application.Interfaces;
using RMS.Application.Models.Auth;

namespace RMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : BaseController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var result = await _authService.LoginAsync(request);

        if (result == null)
        {
            return BadRequestResponse("Invalid email or password");
        }

        return OkResponse(result, "Login successful");
    }
}