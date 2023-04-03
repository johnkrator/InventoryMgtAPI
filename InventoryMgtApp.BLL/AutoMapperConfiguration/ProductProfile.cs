using AutoMapper;
using InventoryMgtApp.DAL.Entities.DTOs.Requests;
using InventoryMgtApp.DAL.Entities.DTOs.Responses;
using InventoryMgtApp.DAL.Entities.Models;

namespace InventoryMgtApp.BLL.AutoMapperConfiguration;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<ProductRequestDto, Product>();
        CreateMap<Product, ProductRequestDto>();
        CreateMap<Product, ProductResponseDto>();
    }
}
