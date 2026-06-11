namespace RMS.Domain.Entities;

public partial class Orderitem
{
    public int OrderItemId { get; set; }

    public int RestaurantId { get; set; }

    public int OrderId { get; set; }

    public int MenuItemId { get; set; }

    public string ItemName { get; set; } = null!;

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public int OrderItemStatusId { get; set; }

    public string? SpecialInstructions { get; set; }

    public DateTime? SentToKitchenAt { get; set; }

    public DateTime? PreparingAt { get; set; }

    public DateTime? ReadyAt { get; set; }

    public DateTime? CancelledAt { get; set; }

    public int? CancelledBy { get; set; }

    public string? CancellationReason { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual User? CancelledByNavigation { get; set; }

    public virtual Menuitem MenuItem { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;

    public virtual Orderitemstatus OrderItemStatus { get; set; } = null!;

    public virtual Restaurant Restaurant { get; set; } = null!;
}
