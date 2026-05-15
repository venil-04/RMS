using System;
using System.Collections.Generic;

namespace RMS.Infrastructure.Persistence.Models;

public partial class Bill
{
    public int Billid { get; set; }

    public int Restaurantid { get; set; }

    public int Orderid { get; set; }

    public string Billnumber { get; set; } = null!;

    public decimal Subtotal { get; set; }

    public decimal Discountamount { get; set; }

    public decimal Taxamount { get; set; }

    public decimal Servicechargeamount { get; set; }

    public decimal Grandtotal { get; set; }

    public int Billstatusid { get; set; }

    public DateTime? Cancelledat { get; set; }

    public int? Cancelledby { get; set; }

    public string? Cancellationreason { get; set; }

    public DateTime Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public int? Createdby { get; set; }

    public int? Updatedby { get; set; }

    public virtual Billstatus Billstatus { get; set; } = null!;

    public virtual User? CancelledbyNavigation { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Restaurant Restaurant { get; set; } = null!;
}
