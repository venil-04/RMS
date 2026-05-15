using System;
using System.Collections.Generic;

namespace RMS.Infrastructure.Persistence.Models;

public partial class Order
{
    public int Orderid { get; set; }

    public int Restaurantid { get; set; }

    public int Tableid { get; set; }

    public string Ordernumber { get; set; } = null!;

    public int Orderstatusid { get; set; }

    public int Openedby { get; set; }

    public int? Closedby { get; set; }

    public DateTime Openedat { get; set; }

    public DateTime? Closedat { get; set; }

    public bool Isdeleted { get; set; }

    public DateTime Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public int? Createdby { get; set; }

    public int? Updatedby { get; set; }

    public virtual Bill? Bill { get; set; }

    public virtual User? ClosedbyNavigation { get; set; }

    public virtual User OpenedbyNavigation { get; set; } = null!;

    public virtual ICollection<Orderitem> Orderitems { get; set; } = new List<Orderitem>();

    public virtual Orderstatus Orderstatus { get; set; } = null!;

    public virtual Restaurant Restaurant { get; set; } = null!;

    public virtual Restauranttable Table { get; set; } = null!;
}
