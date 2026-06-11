namespace RMS.Domain.Entities;

public partial class User
{
    public int UserId { get; set; }

    public int RestaurantId { get; set; }

    public int RoleId { get; set; }

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public string Email { get; set; } = null!;

    public string? MobileNumber { get; set; }

    public string PasswordHash { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();

    public virtual ICollection<Order> OrderClosedByNavigations { get; set; } = new List<Order>();

    public virtual ICollection<Order> OrderOpenedByNavigations { get; set; } = new List<Order>();

    public virtual ICollection<Orderitem> Orderitems { get; set; } = new List<Orderitem>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Restaurant Restaurant { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
