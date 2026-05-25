using AutoMapper;
using RMS.Application.Models.Lookups;
using RMS.Domain.Entities;

namespace RMS.Application.Mappings;

public class LookupMappingProfile : Profile
{
    public LookupMappingProfile()
    {
        CreateMap<Role, RoleResponse>();
    }
}