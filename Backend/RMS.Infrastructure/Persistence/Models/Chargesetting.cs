using System;
using System.Collections.Generic;

namespace RMS.Infrastructure.Persistence.Models;

public partial class Chargesetting
{
    public int Chargesettingid { get; set; }

    public int Restaurantid { get; set; }

    public string Chargename { get; set; } = null!;

    public decimal Chargevalue { get; set; }

    public bool Ispercentage { get; set; }

    public bool Isactive { get; set; }

    public bool Isdeleted { get; set; }

    public DateTime Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public int? Createdby { get; set; }

    public int? Updatedby { get; set; }

    public virtual Restaurant Restaurant { get; set; } = null!;
}
