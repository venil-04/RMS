namespace RMS.Domain.Entities;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int RestaurantId { get; set; }

    public int BillId { get; set; }

    public int PaymentMethodId { get; set; }

    public decimal Amount { get; set; }

    public int PaymentStatusId { get; set; }

    public DateTime PaidAt { get; set; }

    public int ReceivedBy { get; set; }

    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual Bill Bill { get; set; } = null!;

    public virtual Paymentmethod PaymentMethod { get; set; } = null!;

    public virtual Paymentstatus PaymentStatus { get; set; } = null!;

    public virtual User ReceivedByNavigation { get; set; } = null!;

    public virtual Restaurant Restaurant { get; set; } = null!;
}
