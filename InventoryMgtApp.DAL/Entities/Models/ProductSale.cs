using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryMgtApp.DAL.Entities.Models;

public class ProductSale
{
    public Guid ProductId { get; set; }
    public Guid SaleId { get; set; }

    [ForeignKey(nameof(ProductId))] public Product Product { get; set; }

    [ForeignKey(nameof(SaleId))] public Sale Sale { get; set; }
}
