using Microsoft.EntityFrameworkCore;
using RMS.Application.Interfaces;
using RMS.Application.Models.Lookups;
using RMS.Domain.Entities;

namespace RMS.Application.Services;

public class LookupService : ILookupService
{
    private readonly IGenericRepository<Role> _roleRepository;

    public LookupService(IGenericRepository<Role> roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<List<RoleResponse>> GetRolesAsync()
    {
        return await _roleRepository.Query(true)
            .OrderBy(x => x.RoleName)
            .Select(x => new RoleResponse
            {
                RoleId = x.RoleId,
                RoleName = x.RoleName
            })
            .ToListAsync();
    }
}