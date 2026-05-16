using System;
using System.Collections.Generic;

namespace RMS.Domain.Entities;

public partial class Restaurant
{
    public int RestaurantId { get; set; }

    public string RestaurantName { get; set; } = null!;

    public string? Email { get; set; }

    public string? MobileNumber { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();

    public virtual ICollection<Chargesetting> Chargesettings { get; set; } = new List<Chargesetting>();

    public virtual ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

    public virtual ICollection<Menucategory> Menucategories { get; set; } = new List<Menucategory>();

    public virtual ICollection<Menuitem> Menuitems { get; set; } = new List<Menuitem>();

    public virtual ICollection<Orderitem> Orderitems { get; set; } = new List<Orderitem>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Restauranttable> Restauranttables { get; set; } = new List<Restauranttable>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
