using System;
using System.Collections.Generic;

namespace RMS.Domain.Entities;

public partial class Paymentmethod
{
    public int PaymentMethodId { get; set; }

    public string MethodName { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
