using System.ComponentModel.DataAnnotations.Schema;
using InventoryMgtApp.DAL.Enums;

namespace InventoryMgtApp.DAL.Entities.Models;

public class Sale : BaseEntity
{
    public Guid SaleId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public long Quantity { get; set; }
    public Category Category { get; set; }

    [ForeignKey(nameof(AppUser))] public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }

    public virtual ICollection<ProductSale> ProductSales { get; set; }
}
