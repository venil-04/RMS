using AutoMapper;
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
    private readonly IMapper _mapper;

    public AuthService(
        IGenericRepository<User> userRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenService jwtTokenService,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenService = jwtTokenService;
        _mapper = mapper;
    }

    public async Task<LoginResponse?> LoginAsync(LoginRequest request)
    {
        var normalizedEmail = request.Email.Trim().ToLowerInvariant();
        
        var user = await _userRepository.FirstOrDefaultWithIncludeAsync(
            predicate: x =>
                x.Email.ToLower() == normalizedEmail &&
                x.IsActive &&
                !x.IsDeleted,
            include: query => query
                .Include(x => x.Role)
                .ThenInclude(x => x.Rolepermissions)
                .ThenInclude(x => x.Permission)
        );

        if (user is null)
            return null;
        
        var isPasswordValid = _passwordHasher.VerifyPassword(request.Password, user.PasswordHash);

        if (!isPasswordValid)
            return null;

        var token = _jwtTokenService.GenerateToken(user);

        return new LoginResponse
        {
            AccessToken = token,
            User = _mapper.Map<LoggedInUserDto>(user)
        };
    }
}