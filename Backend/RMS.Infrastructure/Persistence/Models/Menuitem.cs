using System;
using System.Collections.Generic;

namespace RMS.Infrastructure.Persistence.Models;

public partial class Menuitem
{
    public int Menuitemid { get; set; }

    public int Restaurantid { get; set; }

    public int Categoryid { get; set; }

    public string? Imageurl { get; set; }

    public string Itemname { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public bool Isavailable { get; set; }

    public bool Isactive { get; set; }

    public bool Isdeleted { get; set; }

    public DateTime Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public int? Createdby { get; set; }

    public int? Updatedby { get; set; }

    public virtual Menucategory Category { get; set; } = null!;

    public virtual ICollection<Orderitem> Orderitems { get; set; } = new List<Orderitem>();

    public virtual Restaurant Restaurant { get; set; } = null!;
}
