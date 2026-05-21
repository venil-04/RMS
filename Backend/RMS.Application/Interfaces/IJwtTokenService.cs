using RMS.Domain.Entities;

namespace RMS.Application.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(User user);
}