namespace RMS.Application.Models.Users;

public class UpdateUserRequest
{
    public int UserId { get; set; }
    public int RestaurantId { get; set; }
    public int RoleId { get; set; }

    public string FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; }

    public string Email { get; set; } = string.Empty;
    public string? MobileNumber { get; set; }
    
    public bool IsActive { get; set; }
}