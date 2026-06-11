namespace RMS.Domain.Entities;

public partial class Billstatus
{
    public int BillStatusId { get; set; }

    public string StatusName { get; set; } = null!;

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();
}
