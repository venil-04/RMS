using RMS.Application.Models.Response;

namespace RMS.Application.Interfaces;

public interface IMenuCategoryService
{
    Task<List<MenuCategoryResponse>> GetAllAsync();
}