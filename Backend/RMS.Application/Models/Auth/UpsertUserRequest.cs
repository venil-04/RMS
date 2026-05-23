namespace RMS.Application.Models.Auth;

public class UpsertUserRequest
{
    public int RestaurantId { get; set; }
    public int RoleId { get; set; }

    public string FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; }

    public string Email { get; set; } = string.Empty;
    public string? MobileNumber { get; set; }
    
    public bool? IsActive { get; set; }

    public string Password { get; set; } = string.Empty;
}