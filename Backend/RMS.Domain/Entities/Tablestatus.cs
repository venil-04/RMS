using System;
using System.Collections.Generic;

namespace RMS.Domain.Entities;

public partial class Tablestatus
{
    public int TableStatusId { get; set; }

    public string StatusName { get; set; } = null!;
}
