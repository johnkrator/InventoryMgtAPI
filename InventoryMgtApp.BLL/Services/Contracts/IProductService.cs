using InventoryMgtApp.DAL.Entities.DTOs.Requests;
using InventoryMgtApp.DAL.Entities.DTOs.Responses;
using Microsoft.AspNetCore.Http;

namespace InventoryMgtApp.BLL.Services.Contracts;

public interface IProductService
{
    Task<Status> CreateNewProduct(ProductRequestDto productRequestDto);
    Task<IEnumerable<ProductResponseDto>> GetAllUserProducts(Guid id);
    Task<IEnumerable<ProductResponseDto>> GetProducts();
    Task<ProductResponseDto> GetProduct(Guid id);
    Task<Status> UpdateProduct(Guid productId, ProductRequestDto productRequestDto);
    Task<Status> DeleteProduct(Guid productId);
    Task<Status> ProductImageUpload(string id, IFormFile file);
}
