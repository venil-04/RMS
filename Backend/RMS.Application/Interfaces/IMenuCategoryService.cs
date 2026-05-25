using RMS.Application.Models.MenuCategories;
using RMS.Application.Models.Response;

namespace RMS.Application.Interfaces;

public interface IMenuCategoryService
{
    Task<MenuCategoryResponse> CreateAsync(CreateMenuCategoryRequest request, int userId);
    Task<MenuCategoryResponse> UpdateAsync(UpdateMenuCategoryRequest request, int userId);
    Task<MenuCategoryResponse> GetByIdAsync(int id);
    Task<IEnumerable<MenuCategoryResponse>> GetAllByRestaurantAsync(int restaurantId);
    Task DeleteAsync(int id, int userId);
}
