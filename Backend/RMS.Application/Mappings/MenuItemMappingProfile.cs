using AutoMapper;
using RMS.Application.Models.MenuItems;
using RMS.Application.Models.Response;
using RMS.Domain.Entities;

namespace RMS.Application.Mappings;

public class MenuItemMappingProfile : Profile
{
    public MenuItemMappingProfile()
    {
        CreateMap<CreateMenuItemRequest, Menuitem>();
        CreateMap<UpdateMenuItemRequest, Menuitem>();
        CreateMap<Menuitem, MenuItemResponse>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName));
    }
}
