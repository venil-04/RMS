using RMS.Application.Models.Auth;
using RMS.Application.Models.Users;

namespace RMS.Application.Interfaces;

public interface IUserService
{
    Task<UserResponse?> UpsertUserAsync(UpsertUserRequest request);
    Task<PagedResponse<UserListItemResponse>> GetUsersAsync(GetUsersRequest request);
}