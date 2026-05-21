using RMS.Application.Models.Auth;

namespace RMS.Application.Interfaces;

public interface IUserService
{
    Task<UserResponse?> UpsertUserAsync(UpsertUserRequest request);
}