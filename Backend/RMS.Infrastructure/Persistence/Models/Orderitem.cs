using System;
using System.Collections.Generic;

namespace RMS.Infrastructure.Persistence.Models;

public partial class Orderitem
{
    public int Orderitemid { get; set; }

    public int Restaurantid { get; set; }

    public int Orderid { get; set; }

    public int Menuitemid { get; set; }

    public string Itemname { get; set; } = null!;

    public int Quantity { get; set; }

    public decimal Unitprice { get; set; }

    public int Orderitemstatusid { get; set; }

    public string? Specialinstructions { get; set; }

    public DateTime? Senttokitchenat { get; set; }

    public DateTime? Preparingat { get; set; }

    public DateTime? Readyat { get; set; }

    public DateTime? Cancelledat { get; set; }

    public int? Cancelledby { get; set; }

    public string? Cancellationreason { get; set; }

    public DateTime Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public int? Createdby { get; set; }

    public int? Updatedby { get; set; }

    public virtual User? CancelledbyNavigation { get; set; }

    public virtual Menuitem Menuitem { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;

    public virtual Orderitemstatus Orderitemstatus { get; set; } = null!;

    public virtual Restaurant Restaurant { get; set; } = null!;
}
