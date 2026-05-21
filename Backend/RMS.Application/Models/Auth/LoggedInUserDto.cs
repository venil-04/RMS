namespace RMS.Application.Models.Auth;

public class LoggedInUserDto
{
    public int UserId { get; set; }
    public int? RestaurantId { get; set; }

    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public int RoleId { get; set; }
    public string RoleName { get; set; } = string.Empty;
}