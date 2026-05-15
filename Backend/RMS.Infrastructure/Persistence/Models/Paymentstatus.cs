using System;
using System.Collections.Generic;

namespace RMS.Infrastructure.Persistence.Models;

public partial class Paymentstatus
{
    public int Paymentstatusid { get; set; }

    public string Statusname { get; set; } = null!;
}
