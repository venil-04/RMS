using Microsoft.AspNetCore.Mvc;
using RMS.Application.Interfaces;

namespace RMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuCategoriesController : BaseController
{
    private readonly IMenuCategoryService _menuCategoryService;

    public MenuCategoriesController (IMenuCategoryService menuCategoryService)
    {
        _menuCategoryService = menuCategoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetMenuCategoriesAsync()
    {
        var menuCategories = await _menuCategoryService.GetAllAsync();
        return OkResponse(menuCategories);
    }
}