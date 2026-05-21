using Microsoft.EntityFrameworkCore;
using RMS.Application.Interfaces;
using RMS.Application.Models.Auth;
using RMS.Domain.Entities;

namespace RMS.Application.Services;

public class AuthService : IAuthService
{
    private readonly IGenericRepository<User> _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenService _jwtTokenService;

    public AuthService(
        IGenericRepository<User> userRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenService jwtTokenService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<LoginResponse?> LoginAsync(LoginRequest request)
    {
        var normalizedEmail = request.Email.Trim().ToLowerInvariant();

        var user = await _userRepository.Query()
            .Include(x => x.Role)
                .ThenInclude(x => x.Rolepermissions)
                    .ThenInclude(x => x.Permission)
            .FirstOrDefaultAsync(x =>
                x.Email.ToLower() == normalizedEmail &&
                x.IsActive == true &&
                x.IsDeleted == false);

        if (user is null)
            return null;
        
        var isPasswordValid = _passwordHasher.VerifyPassword(
            request.Password,
            user.PasswordHash);

        if (!isPasswordValid)
            return null;

        var token = _jwtTokenService.GenerateToken(user);

        return new LoginResponse
        {
            AccessToken = token,
            User = new LoggedInUserDto
            {
                UserId = user.UserId,
                RestaurantId = user.RestaurantId,
                FullName = $"{user.FirstName} {user.LastName}".Trim(),
                Email = user.Email,
                RoleId = user.RoleId,
                RoleName = user.Role.RoleName
            }
        };
    }
}