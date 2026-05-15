using System;
using System.Collections.Generic;

namespace RMS.Infrastructure.Persistence.Models;

public partial class Menucategory
{
    public int Categoryid { get; set; }

    public int Restaurantid { get; set; }

    public string Categoryname { get; set; } = null!;

    public string? Description { get; set; }

    public int Displayorder { get; set; }

    public bool Isactive { get; set; }

    public bool Isdeleted { get; set; }

    public DateTime Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public DateTime? Deletedat { get; set; }

    public int? Createdby { get; set; }

    public int? Updatedby { get; set; }

    public virtual ICollection<Menuitem> Menuitems { get; set; } = new List<Menuitem>();

    public virtual Restaurant Restaurant { get; set; } = null!;
}
