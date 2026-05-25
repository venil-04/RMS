using AutoMapper;
using RMS.Application.Models.Auth;
using RMS.Application.Models.Users;
using RMS.Domain.Entities;

namespace RMS.Application.Mappings;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserResponse>();
        CreateMap<UpdateUserRequest, User>();
        CreateMap<CreateUserRequest, User>();

        CreateMap<User, LoggedInUserDto>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}".Trim()))
            .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.RoleName));
    }
}