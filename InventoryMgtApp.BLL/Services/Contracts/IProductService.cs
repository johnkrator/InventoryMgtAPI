using InventoryMgtApp.DAL.Entities.DTOs;

namespace InventoryMgtApp.BLL.Services.Contracts;

public interface IProductService
{
    Task<Status> CreateNewProduct(ProductRequestDto productRequestDto);
    Task<IEnumerable<ProductResponseDto>> GetAllUserProducts(string id);
    Task<IEnumerable<ProductResponseDto>> GetProducts();
    Task<ProductResponseDto> GetProduct(string id);
    Task<Status> UpdateProduct(string productId, ProductRequestDto productRequestDto);
    Task<Status> DeleteProduct(Guid productId);
}
