namespace RMS.Application.Models.Response;

public class MenuItemResponse
{
    public int MenuItemId { get; set; }
    public int RestaurantId { get; set; }
    public int CategoryId { get; set; }
    public string? ImageUrl { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; }
    public bool IsActive { get; set; }
    public string CategoryName { get; set; } = string.Empty;
}
