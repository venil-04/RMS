namespace RMS.Domain.Entities;

public partial class Role
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<Rolepermission> Rolepermissions { get; set; } = new List<Rolepermission>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
