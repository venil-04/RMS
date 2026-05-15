using System;
using System.Collections.Generic;

namespace RMS.Infrastructure.Persistence.Models;

public partial class Restaurant
{
    public int Restaurantid { get; set; }

    public string Restaurantname { get; set; } = null!;

    public string? Email { get; set; }

    public string? Mobilenumber { get; set; }

    public bool Isactive { get; set; }

    public bool Isdeleted { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public DateTime Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public int? Createdby { get; set; }

    public int? Updatedby { get; set; }

    public virtual ICollection<Menucategory> Menucategories { get; set; } = new List<Menucategory>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
