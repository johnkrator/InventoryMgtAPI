using InventoryMgtApp.DAL.Enums;

namespace InventoryMgtApp.DAL.Entities.DTOs.Requests;

public class SaleDto
{
    public Guid SaleId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public long Quantity { get; set; }
    public Category Category { get; set; }
}