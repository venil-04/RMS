namespace RMS.Application.Models.Auth;

public class UserResponse
{
    public int UserId { get; set; }

    public int RestaurantId { get; set; }
    public int RoleId { get; set; }

    public string FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; }

    public string Email { get; set; } = string.Empty;
    public string? MobileNumber { get; set; }

    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}