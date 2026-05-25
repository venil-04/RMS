using System.ComponentModel.DataAnnotations;

namespace RMS.Application.Models.MenuCategories;

public class CreateMenuCategoryRequest
{
    public int RestaurantId { get; set; }
    public string CategoryName { get; set; } = null!;
    public string? Description { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;
}
