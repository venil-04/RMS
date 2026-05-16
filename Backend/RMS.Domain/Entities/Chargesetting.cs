using System;
using System.Collections.Generic;

namespace RMS.Domain.Entities;

public partial class Chargesetting
{
    public int ChargeSettingId { get; set; }

    public int RestaurantId { get; set; }

    public string ChargeName { get; set; } = null!;

    public decimal ChargeValue { get; set; }

    public bool IsPercentage { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual Restaurant Restaurant { get; set; } = null!;
}
