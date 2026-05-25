using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RMS.Application.Exceptions;
using RMS.Application.Interfaces;
using RMS.Application.Models.Auth;
using RMS.Application.Models.Users;
using RMS.Domain.Entities;

namespace RMS.Application.Services;

public class UserService : IUserService
{
    private readonly IGenericRepository<User> _userRepository;
    private readonly IGenericRepository<Role> _roleRepository;
    private readonly IGenericRepository<Restaurant> _restaurantRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IMapper _mapper;
    
    public UserService(
        IGenericRepository<User> userRepository,
        IGenericRepository<Role> roleRepository,
        IGenericRepository<Restaurant> restaurantRepository,
        IPasswordHasher passwordHasher,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _restaurantRepository = restaurantRepository;
        _passwordHasher = passwordHasher;
        _mapper = mapper;
    }
    
    public async Task<PagedResponse<UserListItemResponse>> GetUsersAsync(GetUsersRequest request)
    {
        var restaurantId = 1; // temporary until taken from logged-in token

        var query = _userRepository.Query()
            .Include(x => x.Role)
            .Where(x => x.RestaurantId == restaurantId && !x.IsDeleted);

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.Trim().ToLower();

            query = query.Where(x =>
                x.FirstName.ToLower().Contains(search) ||
                (x.LastName != null && x.LastName.ToLower().Contains(search)) ||
                x.Email.ToLower().Contains(search) ||
                (x.MobileNumber != null && x.MobileNumber.Contains(search)));
        }

        if (request.RoleId.HasValue)
        {
            query = query.Where(x => x.RoleId == request.RoleId.Value);
        }

        if (request.IsActive.HasValue)
        {
            query = query.Where(x => x.IsActive == request.IsActive.Value);
        }

        var totalRecords = await query.CountAsync();

        var users = await query
            .OrderByDescending(x => x.CreatedAt)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new UserListItemResponse
            {
                UserId = x.UserId,
                RestaurantId = x.RestaurantId,
                RoleId = x.RoleId,
                RoleName = x.Role.RoleName,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                MobileNumber = x.MobileNumber,
                IsActive = x.IsActive,
                CreatedAt = x.CreatedAt
            })
            .ToListAsync();

        return new PagedResponse<UserListItemResponse>
        {
            Items = users,
            TotalRecords = totalRecords,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }
    
    public async Task<UserResponse> CreateUserAsync(CreateUserRequest request)
    {
        var email = request.Email.Trim().ToLower();

        var restaurantExists = await _restaurantRepository.AnyAsync(x =>
            x.RestaurantId == request.RestaurantId &&
            x.IsActive &&
            !x.IsDeleted);

        if (!restaurantExists)
            throw new AppException("Restaurant not found or inactive.", ErrorType.NotFound);

        var roleExists = await _roleRepository.AnyAsync(x =>
            x.RoleId == request.RoleId);

        if (!roleExists)
            throw new AppException("Role not found.", ErrorType.NotFound);

        var userAlreadyExists = await _userRepository.AnyAsync(x =>
            x.Email == email &&
            !x.IsDeleted);

        if (userAlreadyExists)
            throw new AppException("User already exists with this email.", ErrorType.Conflict);

        var user = new User
        {
            RestaurantId = request.RestaurantId,
            RoleId = request.RoleId,

            FirstName = request.FirstName.Trim(),
            LastName = request.LastName?.Trim(),

            Email = email,
            MobileNumber = request.MobileNumber?.Trim(),

            PasswordHash = _passwordHasher.HashPassword(request.Password),

            IsActive = true,
            IsDeleted = false,

            CreatedAt = DateTime.UtcNow
        };

        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();

        return _mapper.Map<UserResponse>(user);
    }
    
    public async Task<UserResponse> UpdateUserAsync(UpdateUserRequest request)
    {
        var email = request.Email.Trim().ToLower();

        var user = await _userRepository.FirstOrDefaultAsync(x =>
            x.UserId == request.UserId &&
            !x.IsDeleted);

        if (user == null)
            throw new AppException("User not found.", ErrorType.NotFound);

        var restaurantExists = await _restaurantRepository.AnyAsync(x =>
            x.RestaurantId == request.RestaurantId &&
            x.IsActive &&
            !x.IsDeleted);

        if (!restaurantExists)
            throw new AppException("Restaurant not found or inactive.", ErrorType.NotFound);

        var roleExists = await _roleRepository.AnyAsync(x =>
            x.RoleId == request.RoleId);

        if (!roleExists)
            throw new AppException("Role not found.", ErrorType.NotFound);

        var emailAlreadyUsed = await _userRepository.AnyAsync(x =>
            x.UserId != request.UserId &&
            x.Email == email &&
            !x.IsDeleted);

        if (emailAlreadyUsed)
            throw new AppException("Email is already used by another user.", ErrorType.Conflict);

        _mapper.Map(request, user);

        user.Email = email;
        user.IsActive = request.IsActive;
        user.UpdatedAt = DateTime.UtcNow;

        await _userRepository.SaveChangesAsync();

        return _mapper.Map<UserResponse>(user);
    }
}