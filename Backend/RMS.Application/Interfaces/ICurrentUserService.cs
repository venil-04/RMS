namespace RMS.Application.Interfaces;

public interface ICurrentUserService
{
    int UserId { get; }
    int RestaurantId { get; }
    string? Email { get; }
    string? RoleName { get; }
    bool IsAuthenticated { get; }
}
