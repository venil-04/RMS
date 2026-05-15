using System;
using System.Collections.Generic;

namespace RMS.Infrastructure.Persistence.Models;

public partial class Paymentmethod
{
    public int Paymentmethodid { get; set; }

    public string Methodname { get; set; } = null!;
}
