namespace RMS.Application.Models.MenuItems;

public class UpdateMenuItemRequest
{
    public int MenuItemId { get; set; }
    public int CategoryId { get; set; }
    public string? ImageUrl { get; set; }
    public string ItemName { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; }
    public bool IsActive { get; set; }
}
