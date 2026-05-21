using RMS.Application.Models.Auth;

namespace RMS.Application.Interfaces;

public interface IAuthService
{
    Task<LoginResponse?> LoginAsync(LoginRequest request);
}