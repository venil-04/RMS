using System.Security.Claims;
using RMS.Application.Exceptions;
using RMS.Application.Interfaces;

namespace RMS.API.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

    public int UserId
    {
        get
        {
            var value = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(value) || !int.TryParse(value, out var id))
            {
                throw new AppException("User ID claim is missing or invalid.", ErrorType.Unauthorized);
            }
            return id;
        }
    }

    public int RestaurantId
    {
        get
        {
            var value = _httpContextAccessor.HttpContext?.User?.FindFirst("restaurant_id")?.Value;
            if (string.IsNullOrEmpty(value) || !int.TryParse(value, out var id))
            {
                throw new AppException("Restaurant ID claim is missing or invalid.", ErrorType.Unauthorized);
            }
            return id;
        }
    }

    public string? Email => _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;

    public string? RoleName => _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value;
}
