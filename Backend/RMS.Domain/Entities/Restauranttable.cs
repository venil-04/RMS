using System;
using System.Collections.Generic;

namespace RMS.Domain.Entities;

public partial class Restauranttable
{
    public int TableId { get; set; }

    public int RestaurantId { get; set; }

    public string TableName { get; set; } = null!;

    public int SeatingCapacity { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Restaurant Restaurant { get; set; } = null!;
}
