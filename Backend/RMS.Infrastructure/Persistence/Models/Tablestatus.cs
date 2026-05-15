using System;
using System.Collections.Generic;

namespace RMS.Infrastructure.Persistence.Models;

public partial class Tablestatus
{
    public int Tablestatusid { get; set; }

    public string Statusname { get; set; } = null!;
}
