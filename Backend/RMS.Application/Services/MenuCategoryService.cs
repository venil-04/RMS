using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RMS.Application.Exceptions;
using RMS.Application.Interfaces;
using RMS.Application.Models.MenuCategories;
using RMS.Application.Models.Response;
using RMS.Domain.Entities;

namespace RMS.Application.Services;

public class MenuCategoryService : IMenuCategoryService
{
    private readonly IGenericRepository<Menucategory> _menuCategoryRepository;
    private readonly IMapper _mapper;

    public MenuCategoryService(IGenericRepository<Menucategory> menuCategoryRepository, IMapper mapper)
    {
        _menuCategoryRepository = menuCategoryRepository;
        _mapper = mapper;
    }

    public async Task<MenuCategoryResponse> CreateAsync(CreateMenuCategoryRequest request, int userId)
    {
        var category = _mapper.Map<Menucategory>(request);
        category.CreatedAt = DateTime.UtcNow;
        category.CreatedBy = userId;
        category.IsDeleted = false;

        await _menuCategoryRepository.AddAsync(category);
        await _menuCategoryRepository.SaveChangesAsync();

        return _mapper.Map<MenuCategoryResponse>(category);
    }

    public async Task<MenuCategoryResponse> UpdateAsync(UpdateMenuCategoryRequest request, int userId)
    {
        var category = await _menuCategoryRepository.GetByIdAsync(request.CategoryId);
        if (category == null || category.IsDeleted)
        {
            throw new AppException("Menu category not found.", ErrorType.NotFound);
        }

        _mapper.Map(request, category);
        category.UpdatedAt = DateTime.UtcNow;
        category.UpdatedBy = userId;

        _menuCategoryRepository.Update(category);
        await _menuCategoryRepository.SaveChangesAsync();

        return _mapper.Map<MenuCategoryResponse>(category);
    }

    public async Task<MenuCategoryResponse> GetByIdAsync(int id)
    {
        var category = await _menuCategoryRepository.GetByIdAsync(id);
        if (category == null || category.IsDeleted)
        {
            throw new AppException("Menu category not found.", ErrorType.NotFound);
        }

        return _mapper.Map<MenuCategoryResponse>(category);
    }

    public async Task<IEnumerable<MenuCategoryResponse>> GetAllByRestaurantAsync(int restaurantId)
    {
        var categories = await _menuCategoryRepository.Query(true)
            .Where(x => x.RestaurantId == restaurantId && !x.IsDeleted)
            .OrderBy(x => x.DisplayOrder)
            .ToListAsync();

        return _mapper.Map<IEnumerable<MenuCategoryResponse>>(categories);
    }

    public async Task DeleteAsync(int id, int userId)
    {
        var category = await _menuCategoryRepository.GetByIdAsync(id);
        if (category == null || category.IsDeleted)
        {
            throw new AppException("Menu category not found.", ErrorType.NotFound);
        }

        category.IsDeleted = true;
        category.DeletedAt = DateTime.UtcNow;
        category.UpdatedBy = userId;

        _menuCategoryRepository.Update(category);
        await _menuCategoryRepository.SaveChangesAsync();
    }
}
