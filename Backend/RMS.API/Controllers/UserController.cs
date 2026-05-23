using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RMS.Application.Constants;
using RMS.Application.Interfaces;
using RMS.Application.Models.Auth;
using RMS.Application.Models.Users;

namespace RMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : BaseController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost("upsertUser")]
    [Authorize(policy : Permission.Users.Upsert)]
    public async Task<IActionResult> UpsertUser(UpsertUserRequest request)
    {
        var result = await _userService.UpsertUserAsync(request);

        if (result == null)
        {
            return BadRequestResponse("System user could not be created. User may already exist, or role/restaurant is invalid.");
        }

        return OkResponse(result, "System user created successfully.");
    }
    
    [HttpPost("getUsers")]
    [Authorize(Policy = Permission.Users.View)]
    public async Task<IActionResult> GetUsers(GetUsersRequest request)
    {
        var result = await _userService.GetUsersAsync(request);
        return OkResponse(result);
    }
}