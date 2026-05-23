namespace RMS.Application.Models.Users;

public class UserListItemResponse
{
    public int UserId { get; set; }

    public int RestaurantId { get; set; }

    public int RoleId { get; set; }
    public string RoleName { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; }

    public string FullName => $"{FirstName} {LastName}".Trim();

    public string Email { get; set; } = string.Empty;
    public string? MobileNumber { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }
}