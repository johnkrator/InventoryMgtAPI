using System.ComponentModel.DataAnnotations.Schema;
using InventoryMgtApp.DAL.Enums;

namespace InventoryMgtApp.DAL.Entities.Models;

public class Product : BaseEntity
{
    public Guid ProductId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string? ProductImagePath { get; set; }
    public Category Category { get; set; }
    public long Quantity { get; set; }
    public decimal Price { get; set; }
    public string BrandName { get; set; }

    [ForeignKey(nameof(AppUser))] public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }

    public virtual ICollection<ProductSale> ProductSales { get; set; }
}
