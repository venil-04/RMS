namespace RMS.Application.Models.MenuCategories;

public class UpdateMenuCategoryRequest
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = null!;
    public string? Description { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
}
