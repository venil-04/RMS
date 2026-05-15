using System;
using System.Collections.Generic;

namespace RMS.Infrastructure.Persistence.Models;

public partial class Billstatus
{
    public int Billstatusid { get; set; }

    public string Statusname { get; set; } = null!;

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();
}
