using AutoMapper;
using InventoryMgtApp.DAL.Entities.DTOs;
using InventoryMgtApp.DAL.Entities.Models;

namespace InventoryMgtApp.BLL.AutoMapperConfiguration;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<ProductRequestDto, Product>()
            .ForMember(dest => dest.ProductId, opt => opt.Ignore())
            .ForMember(dest => dest.AppUserId, opt => opt.MapFrom(src => src.AppUserId));
        CreateMap<Product, ProductRequestDto>()
            .ForMember(dest => dest.AppUserId, opt => opt.MapFrom(src => src.AppUserId));
    }
}
