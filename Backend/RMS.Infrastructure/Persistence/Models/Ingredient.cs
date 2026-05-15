using System;
using System.Collections.Generic;

namespace RMS.Infrastructure.Persistence.Models;

public partial class Ingredient
{
    public int Ingredientid { get; set; }

    public int Restaurantid { get; set; }

    public int Unitid { get; set; }

    public string Ingredientname { get; set; } = null!;

    public decimal? Minimumstocklevel { get; set; }

    public bool Isactive { get; set; }

    public bool Isdeleted { get; set; }

    public DateTime Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public int? Createdby { get; set; }

    public int? Updatedby { get; set; }

    public virtual Restaurant Restaurant { get; set; } = null!;

    public virtual Unit Unit { get; set; } = null!;
}
