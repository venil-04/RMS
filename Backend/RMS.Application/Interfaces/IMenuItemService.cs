using RMS.Application.Models.MenuItems;
using RMS.Application.Models.Response;

namespace RMS.Application.Interfaces;

public interface IMenuItemService
{
    Task<MenuItemResponse> CreateAsync(CreateMenuItemRequest request, int userId);
    Task<MenuItemResponse> UpdateAsync(UpdateMenuItemRequest request, int userId);
    Task<MenuItemResponse> GetByIdAsync(int id);
    Task<IEnumerable<MenuItemResponse>> GetAllByRestaurantAsync(int restaurantId);
    Task<IEnumerable<MenuItemResponse>> GetAllByCategoryAsync(int categoryId);
    Task DeleteAsync(int id, int userId);
}
