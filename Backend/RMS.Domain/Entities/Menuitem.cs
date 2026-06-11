namespace RMS.Domain.Entities;

public partial class Menuitem
{
    public int MenuItemId { get; set; }

    public int RestaurantId { get; set; }

    public int CategoryId { get; set; }

    public string? ImageUrl { get; set; }

    public string ItemName { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public bool IsAvailable { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual Menucategory Category { get; set; } = null!;

    public virtual ICollection<Orderitem> Orderitems { get; set; } = new List<Orderitem>();

    public virtual Restaurant Restaurant { get; set; } = null!;
}
