using AutoMapper;
using InventoryMgtApp.DAL.Entities.DTOs;
using InventoryMgtApp.DAL.Entities.Models;

namespace InventoryMgtApp.BLL.AutoMapperConfiguration;

public class SaleProfile : Profile
{
    public SaleProfile()
    {
        CreateMap<SaleDto, Sale>();
        CreateMap<Sale, SaleDto>();
    }
}
