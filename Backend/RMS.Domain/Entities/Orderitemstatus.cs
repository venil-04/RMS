namespace RMS.Domain.Entities;

public partial class Orderitemstatus
{
    public int OrderItemStatusId { get; set; }

    public string StatusName { get; set; } = null!;

    public virtual ICollection<Orderitem> Orderitems { get; set; } = new List<Orderitem>();
}
