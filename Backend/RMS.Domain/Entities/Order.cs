using System;
using System.Collections.Generic;

namespace RMS.Domain.Entities;

public partial class Order
{
    public int OrderId { get; set; }

    public int RestaurantId { get; set; }

    public int TableId { get; set; }

    public string OrderNumber { get; set; } = null!;

    public int OrderStatusId { get; set; }

    public int OpenedBy { get; set; }

    public int? ClosedBy { get; set; }

    public DateTime OpenedAt { get; set; }

    public DateTime? ClosedAt { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual Bill? Bill { get; set; }

    public virtual User? ClosedByNavigation { get; set; }

    public virtual User OpenedByNavigation { get; set; } = null!;

    public virtual Orderstatus OrderStatus { get; set; } = null!;

    public virtual ICollection<Orderitem> Orderitems { get; set; } = new List<Orderitem>();

    public virtual Restaurant Restaurant { get; set; } = null!;

    public virtual Restauranttable Table { get; set; } = null!;
}
