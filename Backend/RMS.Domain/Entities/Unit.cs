using System;
using System.Collections.Generic;

namespace RMS.Domain.Entities;

public partial class Unit
{
    public int UnitId { get; set; }

    public string UnitName { get; set; } = null!;

    public virtual ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
}
