namespace RMS.Domain.Entities;

public partial class Paymentstatus
{
    public int PaymentStatusId { get; set; }

    public string StatusName { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
