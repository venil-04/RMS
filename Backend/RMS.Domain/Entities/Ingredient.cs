using System;
using System.Collections.Generic;

namespace RMS.Domain.Entities;

public partial class Ingredient
{
    public int IngredientId { get; set; }

    public int RestaurantId { get; set; }

    public int UnitId { get; set; }

    public string IngredientName { get; set; } = null!;

    public decimal? MinimumStockLevel { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual Restaurant Restaurant { get; set; } = null!;

    public virtual Unit Unit { get; set; } = null!;
}
