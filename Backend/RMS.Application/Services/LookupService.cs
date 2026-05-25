using AutoMapper;
using RMS.Application.Interfaces;
using RMS.Application.Models.Lookups;
using RMS.Domain.Entities;

namespace RMS.Application.Services;

public class LookupService : ILookupService
{
    private readonly IGenericRepository<Role> _roleRepository;
    private readonly IMapper _mapper;

    public LookupService(IGenericRepository<Role> roleRepository,IMapper mapper)
    {
        _roleRepository = roleRepository;
        _mapper = mapper;
    }

    public async Task<List<RoleResponse>> GetRolesAsync()
    {
        var roles = await _roleRepository.GetAllAsync();
        roles = roles.OrderBy(x => x.RoleName).ToList();
        return _mapper.Map<List<RoleResponse>>(roles);
    }
}