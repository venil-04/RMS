namespace RMS.Application.Models.Response;

public class MenuCategoryResponse
{
    public int CategoryId { get; set; }
    public int RestaurantId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
}