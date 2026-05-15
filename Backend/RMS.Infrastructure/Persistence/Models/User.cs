using System;
using System.Collections.Generic;

namespace RMS.Infrastructure.Persistence.Models;

public partial class User
{
    public int Userid { get; set; }

    public int Restaurantid { get; set; }

    public int Roleid { get; set; }

    public string Firstname { get; set; } = null!;

    public string? Lastname { get; set; }

    public string Email { get; set; } = null!;

    public string? Mobilenumber { get; set; }

    public string Passwordhash { get; set; } = null!;

    public bool Isactive { get; set; }

    public bool Isdeleted { get; set; }

    public DateTime Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public int? Createdby { get; set; }

    public int? Updatedby { get; set; }

    public virtual Restaurant Restaurant { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
