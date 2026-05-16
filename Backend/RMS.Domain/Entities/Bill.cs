using System;
using System.Collections.Generic;

namespace RMS.Domain.Entities;

public partial class Bill
{
    public int BillId { get; set; }

    public int RestaurantId { get; set; }

    public int OrderId { get; set; }

    public string BillNumber { get; set; } = null!;

    public decimal SubTotal { get; set; }

    public decimal DiscountAmount { get; set; }

    public decimal TaxAmount { get; set; }

    public decimal ServiceChargeAmount { get; set; }

    public decimal GrandTotal { get; set; }

    public int BillStatusId { get; set; }

    public DateTime? CancelledAt { get; set; }

    public int? CancelledBy { get; set; }

    public string? CancellationReason { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual Billstatus BillStatus { get; set; } = null!;

    public virtual User? CancelledByNavigation { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Restaurant Restaurant { get; set; } = null!;
}
