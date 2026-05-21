using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RMS.Application.Interfaces;
using RMS.Domain.Entities;
using RMS.Infrastructure.Auth;

namespace RMS.Infrastructure.Services;

public class JwtTokenService : IJwtTokenService
{
    private readonly JwtSettings _jwtSettings;

    public JwtTokenService(IOptions<JwtSettings> jwtOptions)
    {
        _jwtSettings = jwtOptions.Value;
    }

    public string GenerateToken(User user)
    {
        ArgumentNullException.ThrowIfNull(user);

        var roleName = user.Role?.RoleName ?? string.Empty;

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

            new(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new(ClaimTypes.Name, $"{user.FirstName} {user.LastName}".Trim()),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, roleName),

            new("user_id", user.UserId.ToString()),
            new("restaurant_id", user.RestaurantId.ToString()),
            new("role_id", user.RoleId.ToString()),
            new("role_name", roleName)
        };
        
        // Add permission claims
        if (user.Role?.Rolepermissions != null)
        {
            var permissions = user.Role.Rolepermissions
                .Select(rp => rp.Permission.PermissionName)
                .Distinct();

            foreach (var permission in permissions)
            {
                claims.Add(new Claim("permission", permission));
            }
        }

        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));

        var credentials = new SigningCredentials(
            securityKey,
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}