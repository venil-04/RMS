using System;
using System.Collections.Generic;

namespace RMS.Infrastructure.Persistence.Models;

public partial class Restauranttable
{
    public int Tableid { get; set; }

    public int Restaurantid { get; set; }

    public string Tablename { get; set; } = null!;

    public int Seatingcapacity { get; set; }

    public bool Isactive { get; set; }

    public bool Isdeleted { get; set; }

    public DateTime Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public int? Createdby { get; set; }

    public int? Updatedby { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Restaurant Restaurant { get; set; } = null!;
}
