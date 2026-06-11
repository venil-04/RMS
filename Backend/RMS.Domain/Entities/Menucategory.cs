namespace RMS.Domain.Entities;

public partial class Menucategory
{
    public int CategoryId { get; set; }

    public int RestaurantId { get; set; }

    public string CategoryName { get; set; } = null!;

    public string? Description { get; set; }

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual ICollection<Menuitem> Menuitems { get; set; } = new List<Menuitem>();

    public virtual Restaurant Restaurant { get; set; } = null!;
}
