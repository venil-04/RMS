using System;
using System.Collections.Generic;

namespace RMS.Infrastructure.Persistence.Models;

public partial class Unit
{
    public int Unitid { get; set; }

    public string Unitname { get; set; } = null!;

    public virtual ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
}
