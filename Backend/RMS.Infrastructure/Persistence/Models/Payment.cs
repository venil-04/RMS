using System;
using System.Collections.Generic;

namespace RMS.Infrastructure.Persistence.Models;

public partial class Payment
{
    public int Paymentid { get; set; }

    public int Restaurantid { get; set; }

    public int Billid { get; set; }

    public int Paymentmethodid { get; set; }

    public decimal Amount { get; set; }

    public int Paymentstatusid { get; set; }

    public DateTime Paidat { get; set; }

    public int Receivedby { get; set; }

    public string? Notes { get; set; }

    public DateTime Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public int? Createdby { get; set; }

    public int? Updatedby { get; set; }

    public virtual Bill Bill { get; set; } = null!;

    public virtual Paymentmethod Paymentmethod { get; set; } = null!;

    public virtual Paymentstatus Paymentstatus { get; set; } = null!;

    public virtual User ReceivedbyNavigation { get; set; } = null!;

    public virtual Restaurant Restaurant { get; set; } = null!;
}
