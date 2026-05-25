using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RMS.Application.Exceptions;
using RMS.Application.Interfaces;
using RMS.Application.Models.MenuItems;
using RMS.Application.Models.Response;
using RMS.Domain.Entities;

namespace RMS.Application.Services;

public class MenuItemService : IMenuItemService
{
    private readonly IGenericRepository<Menuitem> _menuItemRepository;
    private readonly IGenericRepository<Menucategory> _menuCategoryRepository;
    private readonly IMapper _mapper;

    public MenuItemService(
        IGenericRepository<Menuitem> menuItemRepository, 
        IGenericRepository<Menucategory> menuCategoryRepository,
        IMapper mapper)
    {
        _menuItemRepository = menuItemRepository;
        _menuCategoryRepository = menuCategoryRepository;
        _mapper = mapper;
    }

    public async Task<MenuItemResponse> CreateAsync(CreateMenuItemRequest request, int userId)
    {
        var category = await _menuCategoryRepository.GetByIdAsync(request.CategoryId);
        if (category == null || category.IsDeleted)
        {
            throw new AppException("Category not found.", ErrorType.NotFound);
        }

        var item = _mapper.Map<Menuitem>(request);
        item.CreatedAt = DateTime.UtcNow;
        item.CreatedBy = userId;
        item.IsDeleted = false;

        await _menuItemRepository.AddAsync(item);
        await _menuItemRepository.SaveChangesAsync();

        return _mapper.Map<MenuItemResponse>(item);
    }

    public async Task<MenuItemResponse> UpdateAsync(UpdateMenuItemRequest request, int userId)
    {
        var item = await _menuItemRepository.GetByIdAsync(request.MenuItemId);
        if (item == null || item.IsDeleted)
        {
            throw new AppException("Menu item not found.", ErrorType.NotFound);
        }

        if (item.CategoryId != request.CategoryId)
        {
            var category = await _menuCategoryRepository.GetByIdAsync(request.CategoryId);
            if (category == null || category.IsDeleted)
            {
                throw new AppException("Category not found.", ErrorType.NotFound);
            }
            item.CategoryId = request.CategoryId;
        }

        _mapper.Map(request, item);
        item.UpdatedAt = DateTime.UtcNow;
        item.UpdatedBy = userId;

        _menuItemRepository.Update(item);
        await _menuItemRepository.SaveChangesAsync();

        return _mapper.Map<MenuItemResponse>(item);
    }

    public async Task<MenuItemResponse> GetByIdAsync(int id)
    {
        var item = await _menuItemRepository.FirstOrDefaultWithIncludeAsync(
            x => x.MenuItemId == id && !x.IsDeleted,
            q => q.Include(m => m.Category),
            true);

        if (item == null)
        {
            throw new AppException("Menu item not found.", ErrorType.NotFound);
        }

        return _mapper.Map<MenuItemResponse>(item);
    }

    public async Task<IEnumerable<MenuItemResponse>> GetAllByRestaurantAsync(int restaurantId)
    {
        var items = await _menuItemRepository.Query(true)
            .Include(x => x.Category)
            .Where(x => x.RestaurantId == restaurantId && !x.IsDeleted)
            .ToListAsync();

        return _mapper.Map<IEnumerable<MenuItemResponse>>(items);
    }

    public async Task<IEnumerable<MenuItemResponse>> GetAllByCategoryAsync(int categoryId)
    {
        var items = await _menuItemRepository.Query(true)
            .Include(x => x.Category)
            .Where(x => x.CategoryId == categoryId && !x.IsDeleted)
            .ToListAsync();

        return _mapper.Map<IEnumerable<MenuItemResponse>>(items);
    }

    public async Task DeleteAsync(int id, int userId)
    {
        var item = await _menuItemRepository.GetByIdAsync(id);
        if (item == null || item.IsDeleted)
        {
            throw new AppException("Menu item not found.", ErrorType.NotFound);
        }

        item.IsDeleted = true;
        item.UpdatedAt = DateTime.UtcNow;
        item.UpdatedBy = userId;

        _menuItemRepository.Update(item);
        await _menuItemRepository.SaveChangesAsync();
    }
}
