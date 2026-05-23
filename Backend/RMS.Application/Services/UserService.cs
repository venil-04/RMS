using Microsoft.EntityFrameworkCore;
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
    
    public UserService(
        IGenericRepository<User> userRepository,
        IGenericRepository<Role> roleRepository,
        IGenericRepository<Restaurant> restaurantRepository,
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _restaurantRepository = restaurantRepository;
        _passwordHasher = passwordHasher;
    }
    
    public async Task<UserResponse?> UpsertUserAsync(UpsertUserRequest request)
    {
        var email = request.Email.Trim().ToLower();

        if (string.IsNullOrWhiteSpace(email))
            return null;

        if (string.IsNullOrWhiteSpace(request.FirstName))
            return null;

        var restaurantExists = await _restaurantRepository.AnyAsync(x =>
            x.RestaurantId == request.RestaurantId &&
            x.IsActive == true &&
            x.IsDeleted == false);

        if (!restaurantExists)
            return null;

        var roleExists = await _roleRepository.AnyAsync(x =>
            x.RoleId == request.RoleId);

        if (!roleExists)
            return null;

        var user = await _userRepository
            .FirstOrDefaultAsync(x =>
                x.Email == email &&
                x.IsDeleted == false);

        if (user == null)
        {
            if (string.IsNullOrWhiteSpace(request.Password))
                return null;

            user = new User
            {
                RestaurantId = request.RestaurantId,
                RoleId = request.RoleId,

                FirstName = request.FirstName.Trim(),
                LastName = request.LastName?.Trim(),

                Email = email,
                MobileNumber = request.MobileNumber?.Trim(),

                IsActive = true,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow
            };

            user.PasswordHash = _passwordHasher.HashPassword(request.Password);

            await _userRepository.AddAsync(user);
        }
        else
        {
            user.RestaurantId = request.RestaurantId;
            user.RoleId = request.RoleId;

            user.FirstName = request.FirstName.Trim();
            user.LastName = request.LastName?.Trim();

            user.MobileNumber = request.MobileNumber?.Trim();

            user.IsActive = request.IsActive.GetValueOrDefault();

            if (!string.IsNullOrWhiteSpace(request.Password))
            {
                user.PasswordHash = _passwordHasher.HashPassword(request.Password);
            }

            _userRepository.Update(user);
        }

        await _userRepository.SaveChangesAsync();

        return new UserResponse
        {
            UserId = user.UserId,

            RestaurantId = user.RestaurantId,
            RoleId = user.RoleId,

            FirstName = user.FirstName,
            LastName = user.LastName,

            Email = user.Email,
            MobileNumber = user.MobileNumber,

            IsActive = user.IsActive,
            CreatedAt = user.CreatedAt
        };
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
}