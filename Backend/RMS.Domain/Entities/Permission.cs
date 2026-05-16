using System;
using System.Collections.Generic;

namespace RMS.Domain.Entities;

public partial class Permission
{
    public int PermissionId { get; set; }

    public string PermissionName { get; set; } = null!;

    public virtual ICollection<Rolepermission> Rolepermissions { get; set; } = new List<Rolepermission>();
}
