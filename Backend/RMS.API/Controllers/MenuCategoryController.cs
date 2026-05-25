using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RMS.API.Filters;
using RMS.Application.Interfaces;
using RMS.Application.Models.MenuCategories;

namespace RMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MenuCategoryController : BaseController
{
    private readonly IMenuCategoryService _menuCategoryService;
    private readonly ICurrentUserService _currentUserService;

    public MenuCategoryController(IMenuCategoryService menuCategoryService, ICurrentUserService currentUserService)
    {
        _menuCategoryService = menuCategoryService;
        _currentUserService = currentUserService;
    }

    [HttpPost("Create")]
    [ServiceFilter(typeof(ValidationFilter<CreateMenuCategoryRequest>))]
    public async Task<IActionResult> Create(CreateMenuCategoryRequest request)
    {
        request.RestaurantId = _currentUserService.RestaurantId;
        var result = await _menuCategoryService.CreateAsync(request, _currentUserService.UserId);
        return OkResponse(result, "Menu category created successfully.");
    }

    [HttpPut("Update")]
    [ServiceFilter(typeof(ValidationFilter<UpdateMenuCategoryRequest>))]
    public async Task<IActionResult> Update(UpdateMenuCategoryRequest request)
    {
        var result = await _menuCategoryService.UpdateAsync(request, _currentUserService.UserId);
        return OkResponse(result, "Menu category updated successfully.");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _menuCategoryService.GetByIdAsync(id);
        return OkResponse(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _menuCategoryService.GetAllByRestaurantAsync(_currentUserService.RestaurantId);
        return OkResponse(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _menuCategoryService.DeleteAsync(id, _currentUserService.UserId);
        return OkResponse(true, "Menu category deleted successfully.");
    }
}
