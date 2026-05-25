using AutoMapper;
using RMS.Application.Models.MenuCategories;
using RMS.Application.Models.Response;
using RMS.Domain.Entities;

namespace RMS.Application.Mappings;

public class MenuCategoryMappingProfile : Profile
{
    public MenuCategoryMappingProfile()
    {
        CreateMap<CreateMenuCategoryRequest, Menucategory>();
        CreateMap<UpdateMenuCategoryRequest, Menucategory>();
        CreateMap<Menucategory, MenuCategoryResponse>();
    }
}
