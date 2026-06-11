using Microsoft.AspNetCore.Mvc;
using RMS.Application.Interfaces;

namespace RMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LookupController : BaseController
{
    private readonly ILookupService _lookupService;

    public LookupController(ILookupService lookupService)
    {
        _lookupService = lookupService;
    }

    [HttpGet("roles")]
    public async Task<IActionResult> GetRoles()
    {
        var result = await _lookupService.GetRolesAsync();
        return OkResponse(result);
    }
}