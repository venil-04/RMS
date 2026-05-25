using RMS.Application.Models.Auth;
using RMS.Application.Models.Users;

namespace RMS.Application.Interfaces;

public interface IUserService
{
    Task<PagedResponse<UserListItemResponse>> GetUsersAsync(GetUsersRequest request);
    Task<UserResponse> CreateUserAsync(CreateUserRequest request);
    Task<UserResponse> UpdateUserAsync(UpdateUserRequest request);
}