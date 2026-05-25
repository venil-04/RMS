using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RMS.API.Filters;
using RMS.Application.Constants;
using RMS.Application.Interfaces;
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
    
    [HttpPost("CreateUser")]
    [Authorize(policy : Permission.Users.Upsert)]
    [ServiceFilter(typeof(ValidationFilter<CreateUserRequest>))]
    public async Task<IActionResult> CreateUser(CreateUserRequest request)
    {
        var result = await _userService.CreateUserAsync(request);

        return OkResponse(result, "System user created successfully.");
    }
    
    [HttpPost("UpdateUser")]
    [Authorize(policy : Permission.Users.Upsert)]
    [ServiceFilter(typeof(ValidationFilter<UpdateUserRequest>))]
    public async Task<IActionResult> UpdateUser(UpdateUserRequest request)
    {
        var result = await _userService.UpdateUserAsync(request);

        return OkResponse(result, "System user created successfully.");
    }
    
    [HttpPost("getUsers")]
    [Authorize(Policy = Permission.Users.View)]
    [ServiceFilter(typeof(ValidationFilter<CreateUserRequest>))]
    public async Task<IActionResult> GetUsers(GetUsersRequest request)
    {
        var result = await _userService.GetUsersAsync(request);
        return OkResponse(result);
    }
}