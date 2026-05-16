using RMS.Application.Interfaces;
using RMS.Application.Models.Response;
using RMS.Domain.Entities;

namespace RMS.Application.Services;

public class MenuCategoryService : IMenuCategoryService
{
    private readonly IGenericRepository<Menucategory> _menuCategoryRepository;

    public MenuCategoryService(IGenericRepository<Menucategory> menuCategoryRepository)
    {
        _menuCategoryRepository = menuCategoryRepository;
    }

    public async Task<List<MenuCategoryResponse>> GetAllAsync()
    {
        var menuCategories = await _menuCategoryRepository
            .FindAsync(x => !x.IsDeleted);

        return menuCategories
            .Select(x => new MenuCategoryResponse
            {
                CategoryId = x.CategoryId,
            })
            .ToList();
    }
}