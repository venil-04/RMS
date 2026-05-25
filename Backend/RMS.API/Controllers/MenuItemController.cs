using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RMS.API.Filters;
using RMS.Application.Interfaces;
using RMS.Application.Models.MenuItems;

namespace RMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MenuItemController : BaseController
{
    private readonly IMenuItemService _menuItemService;
    private readonly ICurrentUserService _currentUserService;

    public MenuItemController(IMenuItemService menuItemService, ICurrentUserService currentUserService)
    {
        _menuItemService = menuItemService;
        _currentUserService = currentUserService;
    }

    [HttpPost("Create")]
    [ServiceFilter(typeof(ValidationFilter<CreateMenuItemRequest>))]
    public async Task<IActionResult> Create(CreateMenuItemRequest request)
    {
        request.RestaurantId = _currentUserService.RestaurantId;
        var result = await _menuItemService.CreateAsync(request, _currentUserService.UserId);
        return OkResponse(result, "Menu item created successfully.");
    }

    [HttpPut("Update")]
    [ServiceFilter(typeof(ValidationFilter<UpdateMenuItemRequest>))]
    public async Task<IActionResult> Update(UpdateMenuItemRequest request)
    {
        var result = await _menuItemService.UpdateAsync(request, _currentUserService.UserId);
        return OkResponse(result, "Menu item updated successfully.");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _menuItemService.GetByIdAsync(id);
        return OkResponse(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _menuItemService.GetAllByRestaurantAsync(_currentUserService.RestaurantId);
        return OkResponse(result);
    }

    [HttpGet("Category/{categoryId}")]
    public async Task<IActionResult> GetAllByCategory(int categoryId)
    {
        var result = await _menuItemService.GetAllByCategoryAsync(categoryId);
        return OkResponse(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _menuItemService.DeleteAsync(id, _currentUserService.UserId);
        return OkResponse(true, "Menu item deleted successfully.");
    }
}
