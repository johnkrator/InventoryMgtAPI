using InventoryMgtApp.DAL.Enums;

namespace InventoryMgtApp.DAL.Entities.DTOs.Requests;

public class ProductRequestDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string? ProductImagePath { get; set; }
    public Category Category { get; set; }
    public long Quantity { get; set; }
    public decimal Price { get; set; }
    public string BrandName { get; set; }
    public Guid AppUserId { get; set; }
}
