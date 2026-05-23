using RMS.Application.Models.Lookups;

namespace RMS.Application.Interfaces;

public interface ILookupService
{
    Task<List<RoleResponse>> GetRolesAsync();
}